using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MoodService.App.AsyncDataServices;
using MoodService.App.DTOs;
using MoodService.App.Models;
using MoodService.App.Services;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoodService.App.EventProcessing
{
  public class EventProcessor : IEventProcessor
  {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
      _scopeFactory = scopeFactory;
      _mapper = mapper;
    }

    public async Task ProcessEvent(string message)
    {
      var eventType = DetermineEvent(message);

      switch (eventType)
      {
        case EventType.TrackPublished:
          await PredictMood(message);
          break;
        default:
          break;
      }
    }

    private async Task PredictMood(string trackPublishedMessage)
    {
      using (var scope = _scopeFactory.CreateScope())
      {
        var spotifyService = scope.ServiceProvider.GetRequiredService<ISpotifyService>();
        var predictionService = scope.ServiceProvider.GetRequiredService<IPredictionService>();
        var messageBusClient = scope.ServiceProvider.GetRequiredService<IMessageBusClient>();

        var publishedTrackDTO = JsonSerializer.Deserialize<PublishedTrackDTO>(trackPublishedMessage);

        try
        {
          var track = _mapper.Map<Track>(publishedTrackDTO);

          var features = await spotifyService.Run(track);
          track.Mood = predictionService.Run(features);
          Console.WriteLine($"--> Track analysed! Mood for track {track.Title} by {track.Artist}: {track.Mood}");

          var trackAnalyzedDTO = _mapper.Map<TrackAnalyzedDTO>(track);
          trackAnalyzedDTO.Event = "Track_Analyzed";

          messageBusClient.PublishTrackAnalyzed(trackAnalyzedDTO);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"--> Could not analyze Track: {ex.Message}");
        }
      }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
      Console.WriteLine("--> Determining Event");

      var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notificationMessage);

      switch (eventType.Event)
      {
        case "Track_Published":
          Console.WriteLine("--> Track Published Event Detected");
          return EventType.TrackPublished;
        default:
          Console.WriteLine("--> Could not determined the event type");
          return EventType.Undetermined;
      }
    }
  }
}
