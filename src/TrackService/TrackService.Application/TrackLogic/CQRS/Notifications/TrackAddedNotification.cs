using MediatR;
using TrackService.Shared.DTOs.TrackDTOs;

namespace TrackService.Application.TrackLogic.CQRS.Notifications
{
  public class TrackAddedNotification : INotification
  {
    public ReadTrackDTO Track { get; set; }
  }
}
