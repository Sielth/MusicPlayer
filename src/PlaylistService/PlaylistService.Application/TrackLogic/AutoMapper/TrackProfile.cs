using AutoMapper;
using PlaylistService.Application.TrackLogic.CQRS.Commands;
using PlaylistService.Application.TrackLogic.CQRS.Responses;
using PlaylistService.Core.Entities;
using PlaylistService.Shared.DTOs.TrackDTOs;
using TrackService;

namespace PlaylistService.Application.TrackLogic.AutoMapper
{
  public class TrackProfile : Profile
  {
    public TrackProfile()
    {
      // Source -> Target
      CreateMap<ReadTrackDTO, Track>();
      CreateMap<Track, ReadTrackDTO>();

      CreateMap<CreateTrackDTO, CreateTrackCommand>();
      CreateMap<CreateTrackCommand, Track>();

      CreateMap<Track, CQRS.Responses.TrackResponse>();
      CreateMap<CQRS.Responses.TrackResponse, ReadTrackDTO>();

      CreateMap<PublishedTrackDTO, CreateTrackCommand>();
      CreateMap<PublishedTrackDTO, Track>();

      CreateMap<GrpcTrackModel, Track>();
    }
  }
}
