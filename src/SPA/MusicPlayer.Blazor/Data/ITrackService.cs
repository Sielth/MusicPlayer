using MusicPlayer.Blazor.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.Blazor.Data
{
  public interface ITrackService
  {
    [Get("/api/tracks")]
    Task<IEnumerable<Track>> GetAllTracks();

    [Post("/api/tracks")]
    Task CreateTrack(Track track);

    [Get("/api/tracks/{mood}")]
    Task<IEnumerable<Track>> GetTracksByMood(string mood);
  }
}
