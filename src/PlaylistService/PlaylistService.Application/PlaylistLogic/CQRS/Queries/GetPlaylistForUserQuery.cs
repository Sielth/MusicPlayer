using MediatR;
using PlaylistService.Application.PlaylistLogic.CQRS.Responses;

namespace PlaylistService.Application.PlaylistLogic.CQRS.Queries
{
  public class GetPlaylistForUserQuery : IRequest<PlaylistResponse>
  {
    public string Id { get; set; }

    public int UserId { get; set; }
  }
}
