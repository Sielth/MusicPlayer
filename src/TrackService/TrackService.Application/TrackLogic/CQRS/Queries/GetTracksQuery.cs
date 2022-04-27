using MediatR;
using System.Collections.Generic;
using TrackService.Application.TrackLogic.CQRS.Responses;

namespace TrackService.Application.TrackLogic.CQRS.Queries
{
  public class GetTracksQuery : IRequest<IEnumerable<CQRSTrackResponse>>
  {
  }
}
