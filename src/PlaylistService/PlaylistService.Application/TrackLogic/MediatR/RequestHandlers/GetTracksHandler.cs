using AutoMapper;
using MediatR;
using PlaylistService.Application.Repo;
using PlaylistService.Application.TrackLogic.CQRS.Queries;
using PlaylistService.Application.TrackLogic.CQRS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlaylistService.Application.TrackLogic.MediatR.RequestHandlers
{
  public class GetTracksHandler : IRequestHandler<GetTracksQuery, IEnumerable<TrackResponse>>
  {
    private readonly ITrackRepo _trackRepo;
    private readonly IMapper _mapper;

    public GetTracksHandler(ITrackRepo trackRepo, IMapper mapper)
    {
      _trackRepo = trackRepo;
      _mapper = mapper;
    }

    public async Task<IEnumerable<TrackResponse>> Handle(GetTracksQuery request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit GetTracks on PlaylistService");

      var tracks = await _trackRepo.GetTracks();

      return _mapper.Map<IEnumerable<TrackResponse>>(tracks);
    }
  }
}
