using PlaylistService.Core.Entities;
using System.Collections.Generic;

namespace PlaylistService.Persistence.Repo
{
    public interface IPlaylistRepo
    {
        bool SaveChanges();

        // Playlists

        void CreatePlaylist(int userId, Playlist playlist);

        IEnumerable<Playlist> GetPlaylistsForUser(int userId);

        Playlist GetPlaylist(int userId, int playlistId);

        // Users

        void CreateUser(User user);

        bool UserExists(int userId);

        bool ExternalUserExists(int externalUserId);

        //void UpdatePlaylist(int playlistId);

        //void DeletePlaylist(int playlistId);

        //// Tracks

        //IEnumerable<Track> GetAllTracksByPlaylistId(int playlistId);
    }
}
