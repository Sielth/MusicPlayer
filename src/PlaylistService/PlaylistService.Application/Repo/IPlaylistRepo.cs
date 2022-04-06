using PlaylistService.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaylistService.Application.Repo
{
    public interface IPlaylistRepo
    {
        Task<bool> SaveChanges();

        // Playlists

        Task CreatePlaylist(int userId, Playlist playlist);

        Task<IEnumerable<Playlist>> GetPlaylistsForUser(int userId);

        Task<Playlist> GetPlaylist(int userId, int playlistId);

        //void UpdatePlaylist(int playlistId);

        //void DeletePlaylist(int playlistId);


        // Users

        Task CreateUser(User user);

        Task<bool> UserExists(int userId);

        Task<bool> ExternalUserExists(int externalUserId);

        //Tracks

        Task<IEnumerable<Track>> GetTracksForPlaylist(int playlistId);

        Task<bool> TrackExists(int trackId);

        Task<bool> ExternalTrackExists(int externalTrackId);

        Task AddTrackToPlaylist(int playlistId, Track track);

        //void RemoveTrackFromPlaylist(int playlistId, int TrackId);
    }
}
