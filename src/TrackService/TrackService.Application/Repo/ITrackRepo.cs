using System.Collections.Generic;
using System.Threading.Tasks;
using TrackService.Core.Entities;

namespace TrackService.Application.Repo
{
  public interface ITrackRepo
  {
    Task CreateTrack(Track track);

    Task<IEnumerable<Track>> GetAllTracks();

    Task<Track> GetTrack(string trackId);
  }
}
