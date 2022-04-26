using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrackService.Application.SyncDataServices.Http;
using TrackService.Application.TrackLogic.CQRS.Notifications;

namespace TrackService.Application.TrackLogic.MediatR.NotificationHandlers.Http
{
  public class SendTrackToPlaylistViaHttp : INotificationHandler<TrackAddedNotification>
  {
    private readonly IPlaylistDataClient _playlistDataClient;

    public SendTrackToPlaylistViaHttp(IPlaylistDataClient playlistDataClient)
    {
      _playlistDataClient = playlistDataClient;
    }

    public async Task Handle(TrackAddedNotification notification, CancellationToken cancellationToken)
    {
      try
      {
        await _playlistDataClient.SendTrackToPlaylist(notification.Track);
      }
      catch (Exception)
      {
        throw new Exception("--> Could not send synchronously");
      }
    }
  }
}
