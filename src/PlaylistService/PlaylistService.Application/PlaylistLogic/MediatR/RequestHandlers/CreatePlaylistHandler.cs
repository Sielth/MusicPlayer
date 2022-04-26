using AutoMapper;
using MediatR;
using PlaylistService.Application.PlaylistLogic.CQRS.Commands;
using PlaylistService.Application.PlaylistLogic.CQRS.Responses;
using PlaylistService.Application.Repo;
using PlaylistService.Core.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlaylistService.Application.PlaylistLogic.MediatR.RequestHandlers
{
  public class CreatePlaylistHandler : IRequestHandler<CreatePlaylistCommand, PlaylistResponse>
  {
    private readonly IPlaylistRepo _playlistRepo;
    private readonly IMapper _mapper;

    public CreatePlaylistHandler(IPlaylistRepo playlistRepo, IMapper mapper)
    {
      _playlistRepo = playlistRepo;
      _mapper = mapper;
    }

    public async Task<PlaylistResponse> Handle(CreatePlaylistCommand request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit CreatePlaylist: {request.UserId}");

      //if (! await _playlistRepo.UserExists(request.UserId)) throw new KeyNotFoundException(); TODO: Remove comment when you sync UserService to PlaylistService

      var playlistModel = _mapper.Map<Playlist>(request);

      await _playlistRepo.CreatePlaylist(playlistModel.UserId, playlistModel);

      var playlistResponse = _mapper.Map<PlaylistResponse>(playlistModel);

      return playlistResponse;
    }
  }
}
