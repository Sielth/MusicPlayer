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
  public class GetPlaylistsForUserHandler : IRequestHandler<GetPlaylistsForUserQuery, IEnumerable<PlaylistResponse>>
  {
    private readonly IPlaylistRepo _playlistRepo;
    private readonly IMapper _mapper;

    public GetPlaylistsForUserHandler(IPlaylistRepo playlistRepo, IMapper mapper)
    {
      _playlistRepo = playlistRepo;
      _mapper = mapper;
    }

    public async Task<IEnumerable<PlaylistResponse>> Handle(GetPlaylistsForUserQuery request, CancellationToken cancellationToken)
    {
      Console.WriteLine($"--> Hit GetPlaylistsForUser: {request.UserId}");

      //if (! await _playlistRepo.UserExists(request.UserId)) throw new KeyNotFoundException(); TODO: Remove comment when you sync UserService to PlaylistService

      var playlists = await _playlistRepo.GetPlaylistsForUser(request.UserId);

      return _mapper.Map<IEnumerable<PlaylistResponse>>(playlists);
    }
  }
}
