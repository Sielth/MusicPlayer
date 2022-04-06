using AutoMapper;
using MediatR;
using PlaylistService.Application.CQRS.Queries;
using PlaylistService.Application.CQRS.Responses;
using PlaylistService.Application.Repo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PlaylistService.Application.Handlers.PlaylistHandlers
{
    public class GetPlaylistHandler : IRequestHandler<GetPlaylistQuery, PlaylistResponse>
    {
        private readonly IPlaylistRepo _playlistRepo;
        private readonly IMapper _mapper;

        public GetPlaylistHandler(IPlaylistRepo playlistRepo, IMapper mapper)
        {
            _playlistRepo = playlistRepo;
            _mapper = mapper;
        }

        public async Task<PlaylistResponse> Handle(GetPlaylistQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"--> Hit GetPlaylist: {request.Id} / {request.UserId}");

            //if (! await _playlistRepo.UserExists(request.UserId)) throw new KeyNotFoundException(); TODO: Remove comment when you sync UserService to PlaylistService

            var playlist = await _playlistRepo.GetPlaylist(request.UserId, request.Id);

            if (playlist == null) throw new KeyNotFoundException();

            return _mapper.Map<PlaylistResponse>(playlist);
        }
    }
}
