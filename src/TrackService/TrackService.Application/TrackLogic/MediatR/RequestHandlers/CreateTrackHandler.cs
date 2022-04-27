using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrackService.Application.Repo;
using TrackService.Application.TrackLogic.CQRS.Commands;
using TrackService.Application.TrackLogic.CQRS.Responses;
using TrackService.Core.Entities;

namespace TrackService.Application.TrackLogic.MediatR.RequestHandlers
{
  public class CreateTrackHandler : IRequestHandler<CreateTrackCommand, CQRSTrackResponse>
  {
    private readonly ITrackRepo _trackRepo;
    private readonly IMapper _mapper;

    public CreateTrackHandler(IMapper mapper, ITrackRepo trackRepo)
    {
      _mapper = mapper;
      _trackRepo = trackRepo;
    }


    public async Task<CQRSTrackResponse> Handle(CreateTrackCommand request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit CreateTrack");

      if (request == null) throw new ArgumentNullException(nameof(request));

      var trackModel = _mapper.Map<Track>(request);

      await _trackRepo.CreateTrack(trackModel);

      var trackResponse = _mapper.Map<CQRSTrackResponse>(trackModel);

      return trackResponse;
    }
  }
}
