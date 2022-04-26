using PlaylistService.Application.PlaylistLogic.CQRS.Commands;
using PlaylistService.Application.PlaylistLogic.CQRS.Responses;
using PlaylistService.Core.Entities;
using PlaylistService.Shared.DTOs.PlaylistDTOs;
using AutoMapper;
using PlaylistService.Shared.DTOs.TrackDTOs;

namespace PlaylistService.Application.PlaylistLogic.AutoMapper
{
  public class PlaylistProfile : Profile
  {
    public PlaylistProfile()
    {
      // Source -> Target
      CreateMap<PlaylistCreateDTO, CreatePlaylistCommand>();
      CreateMap<CreatePlaylistCommand, Playlist>();

      //CreateMap<TrackReadDTO, AddTrackToPlaylistCommand>();
      CreateMap<TrackReadDTO, Track>();
      CreateMap<AddTrackToPlaylistCommand, Playlist>();

      CreateMap<Playlist, PlaylistResponse>();
      CreateMap<PlaylistResponse, PlaylistReadDTO>();
    }
  }
}
