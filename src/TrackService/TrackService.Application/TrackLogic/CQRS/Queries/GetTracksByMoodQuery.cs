using MediatR;
using System.Collections.Generic;
using TrackService.Application.TrackLogic.CQRS.Responses;

namespace TrackService.Application.TrackLogic.CQRS.Queries
{
  public class GetTracksByMoodQuery : IRequest<IEnumerable<CQRSTrackResponse>>
  {
    public string Mood { get; set; }
  }
}
