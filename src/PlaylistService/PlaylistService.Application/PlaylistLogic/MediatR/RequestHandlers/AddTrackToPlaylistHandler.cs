using AutoMapper;
using MediatR;
using PlaylistService.Application.PlaylistLogic.CQRS.Commands;
using PlaylistService.Application.PlaylistLogic.CQRS.Responses;
using PlaylistService.Application.Repo;
using PlaylistService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PlaylistService.Application.PlaylistLogic.MediatR.RequestHandlers
{
  public class AddTrackToPlaylistHandler : IRequestHandler<AddTrackToPlaylistCommand, PlaylistResponse>
  {
    private readonly IPlaylistRepo _playlistRepo;
    private readonly ITrackRepo _trackRepo;
    private readonly IMapper _mapper;

    public AddTrackToPlaylistHandler(IPlaylistRepo playlistRepo, IMapper mapper, ITrackRepo trackRepo)
    {
      _playlistRepo = playlistRepo;
      _mapper = mapper;
      _trackRepo = trackRepo;
    }

    public async Task<PlaylistResponse> Handle(AddTrackToPlaylistCommand request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit AddTrackToPlaylist: {request.UserId}");

      //if (! await _playlistRepo.UserExists(request.UserId)) throw new KeyNotFoundException(); TODO: Remove comment when you sync UserService to PlaylistService

      //if (!await _trackRepo.TrackExists(request.Track.Id)) throw new KeyNotFoundException(); TODO: Remove comment

      var track = await _trackRepo.GetTrack(request.TrackId);

      await _playlistRepo.AddTrackToPlaylist(request.UserId, request.Id, track);

      return new PlaylistResponse { Id = request.Id, UserId = request.UserId};
    }
  }
}
