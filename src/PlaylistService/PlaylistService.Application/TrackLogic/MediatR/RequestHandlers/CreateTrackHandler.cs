using AutoMapper;
using MediatR;
using PlaylistService.Application.Repo;
using PlaylistService.Application.TrackLogic.CQRS.Commands;
using PlaylistService.Application.TrackLogic.CQRS.Responses;
using PlaylistService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlaylistService.Application.TrackLogic.MediatR.RequestHandlers
{
  public class CreateTrackHandler : IRequestHandler<CreateTrackCommand, TrackResponse>
  {
    private readonly ITrackRepo _trackRepo;
    private readonly IMapper _mapper;

    public CreateTrackHandler(IMapper mapper, ITrackRepo trackRepo)
    {
      _mapper = mapper;
      _trackRepo = trackRepo;
    }


    public async Task<TrackResponse> Handle(CreateTrackCommand request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit CreateTrack {request.Id} on PlaylistService");

      if (request == null) throw new ArgumentNullException(nameof(request));

      var trackModel = _mapper.Map<Track>(request);

      await _trackRepo.CreateTrack(trackModel);

      var trackResponse = _mapper.Map<TrackResponse>(trackModel);

      return trackResponse;
    }
  }
}
