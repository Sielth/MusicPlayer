using MediatR;
using PlaylistService.Application.CQRS.Responses;
using System.Collections.Generic;

namespace PlaylistService.Application.CQRS.Queries
{
    public class GetPlaylistsForUserQuery : IRequest<IEnumerable<PlaylistResponse>>
    {
        public int UserId { get; set; }
    }
}
