using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackService.Application.Repo;
using TrackService.Application.TrackLogic.CQRS.Queries;
using TrackService.Application.TrackLogic.CQRS.Responses;

namespace TrackService.Application.TrackLogic.MediatR.RequestHandlers
{
  public class GetTracksHandler : IRequestHandler<GetTracksQuery, IEnumerable<CQRSTrackResponse>>
  {
    private readonly ITrackRepo _trackRepo;
    private readonly IMapper _mapper;

    public GetTracksHandler(ITrackRepo trackRepo, IMapper mapper)
    {
      _trackRepo = trackRepo;
      _mapper = mapper;
    }

    public async Task<IEnumerable<CQRSTrackResponse>> Handle(GetTracksQuery request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit GetTracks");

      var tracks = await _trackRepo.GetTracks();

      return _mapper.Map<IEnumerable<CQRSTrackResponse>>(tracks);
    }
  }
}
