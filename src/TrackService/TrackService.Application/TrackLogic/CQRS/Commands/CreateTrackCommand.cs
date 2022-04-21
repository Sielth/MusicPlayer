using MediatR;
using TrackService.Application.TrackLogic.CQRS.Responses;

namespace TrackService.Application.TrackLogic.CQRS.Commands
{
  public class CreateTrackCommand : IRequest<TrackResponse>
  {
    public string Title { get; set; }

    public string Artist { get; set; }

    public string Genre { get; set; }
  }
}
