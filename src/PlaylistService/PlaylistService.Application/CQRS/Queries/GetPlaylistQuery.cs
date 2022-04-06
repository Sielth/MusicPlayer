using MediatR;
using PlaylistService.Application.CQRS.Responses;

namespace PlaylistService.Application.CQRS.Queries
{
    public class GetPlaylistQuery : IRequest<PlaylistResponse>
    {
        public int Id { get; set; }

        public int UserId { get; set; }
    }
}
