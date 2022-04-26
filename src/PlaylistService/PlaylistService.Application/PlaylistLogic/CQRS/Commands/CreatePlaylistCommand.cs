using MediatR;
using PlaylistService.Application.PlaylistLogic.CQRS.Responses;

namespace PlaylistService.Application.PlaylistLogic.CQRS.Commands
{
  public class CreatePlaylistCommand : IRequest<PlaylistResponse>
  {
    public string Title { get; set; }

    public int UserId { get; set; }
  }
}
