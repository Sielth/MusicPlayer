using AutoMapper;
using MediatR;
using PlaylistService.Application.Repo;
using PlaylistService.Application.TrackLogic.CQRS.Queries;
using PlaylistService.Application.TrackLogic.CQRS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlaylistService.Application.TrackLogic.MediatR.RequestHandlers
{
  public class GetTrackHandler : IRequestHandler<GetTrackQuery, TrackResponse>
  {
    private readonly ITrackRepo _trackRepo;
    private readonly IMapper _mapper;

    public GetTrackHandler(ITrackRepo trackRepo, IMapper mapper)
    {
      _trackRepo = trackRepo;
      _mapper = mapper;
    }

    public async Task<TrackResponse> Handle(GetTrackQuery request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit GetTrack: {request.Id} on PlaylistService");

      var track = await _trackRepo.GetTrack(request.Id);

      if (track == null) throw new ArgumentNullException(nameof(track));

      return _mapper.Map<TrackResponse>(track);
    }
  }
}
