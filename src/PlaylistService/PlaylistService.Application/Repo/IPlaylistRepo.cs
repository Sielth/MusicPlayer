using PlaylistService.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaylistService.Application.Repo
{
  public interface IPlaylistRepo
  {
    Task CreatePlaylist(int userId, Playlist playlist);

    Task<IEnumerable<Playlist>> GetPlaylistsForUser(int userId);

    Task<Playlist> GetPlaylistForUser(int userId, string playlistId);

    Task AddTrackToPlaylist(int userId, string playlistId, Track track);
  }
}
