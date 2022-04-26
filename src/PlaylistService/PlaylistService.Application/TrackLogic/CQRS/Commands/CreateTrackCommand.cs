using MediatR;
using PlaylistService.Application.TrackLogic.CQRS.Responses;

namespace PlaylistService.Application.TrackLogic.CQRS.Commands
{
  public class CreateTrackCommand : IRequest<TrackResponse>
  {
    public string Id { get; set; }

    public string Title { get; set; }

    public string Artist { get; set; }

    public string Genre { get; set; }
  }
}
