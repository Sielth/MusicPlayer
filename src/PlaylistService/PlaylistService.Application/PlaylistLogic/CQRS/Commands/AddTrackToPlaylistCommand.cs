using MediatR;
using PlaylistService.Application.PlaylistLogic.CQRS.Responses;
using PlaylistService.Shared.DTOs.TrackDTOs;

namespace PlaylistService.Application.PlaylistLogic.CQRS.Commands
{
  public class AddTrackToPlaylistCommand : IRequest<PlaylistResponse>
  {
    public string Id { get; set; }

    public int UserId { get; set; }

    public TrackReadDTO Track { get; set; }
  }
}
