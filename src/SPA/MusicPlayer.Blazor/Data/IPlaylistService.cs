using MusicPlayer.Blazor.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.Blazor.Data
{
  public interface IPlaylistService
  {
    [Get("/api/p/{userId}/playlists")]
    Task<IEnumerable<Playlist>> GetPlaylistsForUser(int userId);

    [Get("/api/p/{userId}/playlists/{**playlistId}")]
    Task<Playlist> GetPlaylistForUser(int userId, string playlistId);

    [Put("/api/p/{userId}/playlists/{**playlistId}")]
    Task AddTrackToPlaylist(int userId, string playlistId, string trackId);

    [Post("/api/p/{userId}/playlists")]
    Task<Playlist> CreatePlaylist(int userId, Playlist playlist);
  }
}
