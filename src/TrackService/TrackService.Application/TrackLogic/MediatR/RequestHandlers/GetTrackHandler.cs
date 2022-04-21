using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrackService.Application.Repo;
using TrackService.Application.TrackLogic.CQRS.Queries;
using TrackService.Application.TrackLogic.CQRS.Responses;

namespace TrackService.Application.TrackLogic.MediatR.RequestHandlers
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
      Console.WriteLine($"--> Hit GetTrack: {request.Id}");

      var track = await _trackRepo.GetTrack(request.Id);

      if (track == null) throw new ArgumentNullException(nameof(track));

      return _mapper.Map<TrackResponse>(track);
    }
  }
}
