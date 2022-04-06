using MediatR;
using PlaylistService.Application.CQRS.Responses;

namespace PlaylistService.Application.CQRS.Commands
{
    public class CreatePlaylistCommand : IRequest<PlaylistResponse>
    {
        public string Title { get; set; }

        public int UserId { get; set; }
    }
}
