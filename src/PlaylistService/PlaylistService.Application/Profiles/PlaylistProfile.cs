using AutoMapper;
using PlaylistService.Application.CQRS.Commands;
using PlaylistService.Application.CQRS.Responses;
using PlaylistService.Core.Entities;
using PlaylistService.Shared.DTOs.PlaylistDTOs;

namespace PlaylistService.Application.Profiles
{
    public class PlaylistProfile : Profile
    {
        public PlaylistProfile()
        {
            // Source -> Target
            CreateMap<PlaylistCreateDTO, CreatePlaylistCommand>();
            CreateMap<CreatePlaylistCommand, Playlist>();

            CreateMap<Playlist, PlaylistResponse>();
            CreateMap<PlaylistResponse, PlaylistReadDTO>();
        }
    }
}
