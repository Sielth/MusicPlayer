using PlaylistService.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaylistService.Application.Repo
{
  public interface ITrackRepo
  {
    Task CreateTrack(Track track);

    Task<IEnumerable<Track>> GetTracks();

    Task<bool> TrackExists(string trackId);

    Task<Track> GetTrack(string trackId);
  }
}
