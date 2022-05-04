using System.Collections.Generic;
using System.Threading.Tasks;
using TrackService.Core.Entities;

namespace TrackService.Application.Repo
{
  public interface ITrackRepo
  {
    Task CreateTrack(Track track);

    Task<IEnumerable<Track>> GetTracks();

    Task<IEnumerable<Track>> GetTracksByMood(string mood);

    Task<Track> GetTrack(string trackId);

    Task UpdateMoodOfTrack(Track track);
  }
}
