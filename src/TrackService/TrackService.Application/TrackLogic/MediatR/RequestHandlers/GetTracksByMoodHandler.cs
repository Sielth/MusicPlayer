using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrackService.Application.Repo;
using TrackService.Application.TrackLogic.CQRS.Queries;
using TrackService.Application.TrackLogic.CQRS.Responses;

namespace TrackService.Application.TrackLogic.MediatR.RequestHandlers
{
  public class GetTracksByMoodHandler : IRequestHandler<GetTracksByMoodQuery, IEnumerable<CQRSTrackResponse>>
  {
    private readonly ITrackRepo _trackRepo;
    private readonly IMapper _mapper;

    public GetTracksByMoodHandler(ITrackRepo trackRepo, IMapper mapper)
    {
      _trackRepo = trackRepo;
      _mapper = mapper;
    }

    public async Task<IEnumerable<CQRSTrackResponse>> Handle(GetTracksByMoodQuery request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit GetTracksByMood");

      var tracks = await _trackRepo.GetTracksByMood(request.Mood);

      return _mapper.Map<IEnumerable<CQRSTrackResponse>>(tracks);
    }
  }
}
