using AutoMapper;
using MediatR;
using PlaylistService.Application.PlaylistLogic.CQRS.Queries;
using PlaylistService.Application.PlaylistLogic.CQRS.Responses;
using PlaylistService.Application.Repo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PlaylistService.Application.PlaylistLogic.MediatR.RequestHandlers
{
  public class GetPlaylistForUserHandler : IRequestHandler<GetPlaylistForUserQuery, PlaylistResponse>
  {
    private readonly IPlaylistRepo _playlistRepo;
    private readonly IMapper _mapper;

    public GetPlaylistForUserHandler(IPlaylistRepo playlistRepo, IMapper mapper)
    {
      _playlistRepo = playlistRepo;
      _mapper = mapper;
    }

    public async Task<PlaylistResponse> Handle(GetPlaylistForUserQuery request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit GetPlaylist: {request.Id} / {request.UserId}");

      //if (! await _playlistRepo.UserExists(request.UserId)) throw new KeyNotFoundException(); TODO: Remove comment when you sync UserService to PlaylistService

      var playlist = await _playlistRepo.GetPlaylistForUser(request.UserId, request.Id);

      if (playlist == null) throw new KeyNotFoundException();

      return _mapper.Map<PlaylistResponse>(playlist);
    }
  }
}
