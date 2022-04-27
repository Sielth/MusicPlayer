using MediatR;
using TrackService.Application.TrackLogic.CQRS.Responses;

namespace TrackService.Application.TrackLogic.CQRS.Queries
{
  public class GetTrackQuery : IRequest<CQRSTrackResponse>
  {
    public string Id { get; set; }
  }
}
