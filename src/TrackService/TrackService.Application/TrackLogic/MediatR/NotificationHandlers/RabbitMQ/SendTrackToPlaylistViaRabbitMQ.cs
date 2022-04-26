using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrackService.Application.AsyncDataServices;
using TrackService.Application.TrackLogic.CQRS.Notifications;
using TrackService.Shared.DTOs.TrackDTOs;

namespace TrackService.Application.TrackLogic.MediatR.NotificationHandlers.RabbitMQ
{
  public class SendTrackToPlaylistViaRabbitMQ : INotificationHandler<TrackAddedNotification>
  {
    private readonly IMessageBusClient _messageBusClient;
    private readonly IMapper _mapper;

    public SendTrackToPlaylistViaRabbitMQ(IMessageBusClient messageBusClient, IMapper mapper)
    {
      _messageBusClient = messageBusClient;
      _mapper = mapper;
    }

    public Task Handle(TrackAddedNotification notification, CancellationToken cancellationToken)
    {
      try
      {
        var trackPublished = _mapper.Map<PublishedTrackDTO>(notification.Track);
        trackPublished.Event = "Track_Published";
        _messageBusClient.PublishNewTrack(trackPublished);
      }
      catch (Exception)
      {
        throw new Exception("--> Could not send asynchronously");
      }

      return Task.CompletedTask;
    }
  }
}
