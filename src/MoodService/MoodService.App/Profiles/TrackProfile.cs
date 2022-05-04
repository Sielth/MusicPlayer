using AutoMapper;
using MoodService.App.DTOs;
using MoodService.App.Models;
using System;

namespace MoodService.App.Profiles
{
  public class TrackProfile : Profile
  {
    public TrackProfile()
    {
      CreateMap<PublishedTrackDTO, Track>();
      CreateMap<Track, TrackAnalyzedDTO>()
        .ForMember(dest => dest.Mood, opt => opt.MapFrom(src => Enum.GetName(typeof(MoodType), src.Mood)));
    }
  }
}