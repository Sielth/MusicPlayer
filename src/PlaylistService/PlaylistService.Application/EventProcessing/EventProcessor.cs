using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PlaylistService.Application.Repo;
using PlaylistService.Application.TrackLogic.CQRS.Commands;
using PlaylistService.Core.Entities;
using PlaylistService.Shared.DTOs;
using PlaylistService.Shared.DTOs.TrackDTOs;
using System;
using System.Text.Json;

namespace PlaylistService.Application.EventProcessing
{
  public class EventProcessor : IEventProcessor
  {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper, IMediator mediator)
    {
      _scopeFactory = scopeFactory;
      _mapper = mapper;
      _mediator = mediator;
    }

    public void ProcessEvent(string message)
    {
      var eventType = DetermineEvent(message);

      switch (eventType)
      {
        case EventType.PlatformPublished:
          AddTrack(message);
          break;
        default:
          break;
      }
    }

    private void AddTrack(string platformPublishedMessage)
    {
      using (var scope = _scopeFactory.CreateScope())
      {
        var repo = scope.ServiceProvider.GetRequiredService<ITrackRepo>();

        var publishedTrackDTO = JsonSerializer.Deserialize<PublishedTrackDTO>(platformPublishedMessage);

        try
        {
          var track = _mapper.Map<Track>(publishedTrackDTO);

          //TODO: If !repo.PlatformExists
          repo.CreateTrack(track);
          Console.WriteLine($"--> Track added!");


          // _mediator.Send(request); ?? TODO: Figure out if I can use MediatR
        }
        catch (Exception ex)
        {
          Console.WriteLine($"--> Could not add Track to DB: {ex.Message}");
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
          return EventType.PlatformPublished;
        default:
          Console.WriteLine("--> Could not determined the event type");
          return EventType.Undetermined;
      }
    }
  }

  enum EventType
  {
    PlatformPublished,
    Undetermined
  }
}
