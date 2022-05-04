using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using TrackService.Application.Repo;
using TrackService.Core.Entities;
using TrackService.Shared.DTOs;
using TrackService.Shared.DTOs.TrackDTOs;

namespace TrackService.Application.EventProcessing
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

    public void ProcessEvent(string message)
    {
      var eventType = DetermineEvent(message);

      switch (eventType)
      {
        case EventType.TrackAnalyzed:
          UpdateTrack(message);
          break;
        default:
          break;
      }
    }

    private void UpdateTrack(string trackAnalyzedMessage)
    {
      using (var scope = _scopeFactory.CreateScope())
      {
        var repo = scope.ServiceProvider.GetRequiredService<ITrackRepo>();

        var analyzedTrackDTO = JsonSerializer.Deserialize<AnalyzedTrackDTO>(trackAnalyzedMessage);

        try
        {
          var track = _mapper.Map<Track>(analyzedTrackDTO);

          Console.WriteLine(track.Id + track.Title + track.Mood);

          //TODO: If !repo.PlatformExists
          repo.UpdateMoodOfTrack(track);
          Console.WriteLine($"--> Track updated! {track.Id} / {track.Mood}");

          // _mediator.Send(request); ?? TODO: Figure out if I can use MediatR
        }
        catch (Exception ex)
        {
          Console.WriteLine($"--> Could not update Track: {ex.Message}");
        }
      }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
      Console.WriteLine("--> Determining Event");

      var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notificationMessage);

      switch (eventType.Event)
      {
        case "Track_Analyzed":
          Console.WriteLine("--> Track Analyzed Event Detected");
          return EventType.TrackAnalyzed;
        default:
          Console.WriteLine("--> Could not determined the event type");
          return EventType.Undetermined;
      }
    }
  }
}
