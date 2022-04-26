using MediatR;
using PlaylistService.Application.TrackLogic.CQRS.Responses;

namespace PlaylistService.Application.TrackLogic.CQRS.Queries
{
  public class GetTrackQuery : IRequest<TrackResponse>
  {
    public string Id { get; set; }
  }
}
