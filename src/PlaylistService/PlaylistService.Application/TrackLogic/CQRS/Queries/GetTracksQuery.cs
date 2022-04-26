using MediatR;
using PlaylistService.Application.TrackLogic.CQRS.Responses;
using System.Collections.Generic;

namespace PlaylistService.Application.TrackLogic.CQRS.Queries
{
  public class GetTracksQuery : IRequest<IEnumerable<TrackResponse>>
  {
  }
}
