using MediatR;
using PlaylistService.Application.PlaylistLogic.CQRS.Responses;
using System.Collections.Generic;

namespace PlaylistService.Application.PlaylistLogic.CQRS.Queries
{
  public class GetPlaylistsForUserQuery : IRequest<IEnumerable<PlaylistResponse>>
  {
    public int UserId { get; set; }
  }
}
