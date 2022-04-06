using Microsoft.EntityFrameworkCore;
using PlaylistService.Application.Repo;
using PlaylistService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistService.Persistence.Repo
{
    public class PlaylistRepo : IPlaylistRepo //TODO: PrepDb class when grpcClient is done
    {
        private readonly AppDbContext _context;

        public PlaylistRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        // Playlist

        public async Task CreatePlaylist(int userId, Playlist playlist)
        {
            if (playlist == null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            playlist.UserId = userId;
            await _context.Playlists.AddAsync(playlist);
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsForUser(int userId)
        {
            return await _context.Playlists
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Title)
                .ToListAsync();
        }

        public async Task<Playlist> GetPlaylist(int userId, int playlistId)
        {
            return await _context.Playlists
                .Where(p => p.UserId == userId && p.Id == playlistId)
                .FirstOrDefaultAsync();
        }

        // User

        public async Task CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _context.Users.AddAsync(user);
        }

        public async Task<bool> UserExists(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<bool> ExternalUserExists(int externalUserId)
        {
            return await _context.Users.AnyAsync(u => u.ExternalId == externalUserId);
        }

        // Tracks

        public async Task<IEnumerable<Track>> GetTracksForPlaylist(int playlistId)
        {
            return await _context.Tracks
                .Where(t => t.PlaylistId == playlistId)
                .OrderBy(t => t.Title)
                .ToListAsync();
        }

        public async Task<bool> TrackExists(int trackId)
        {
            return await _context.Tracks.AnyAsync(t => t.Id == trackId);
        }

        public async Task<bool> ExternalTrackExists(int externalTrackId)
        {
            return await _context.Tracks.AnyAsync(t => t.ExternalId == externalTrackId);
        }

        public async Task AddTrackToPlaylist(int playlistId, Track track)
        {
            if (track == null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            track.PlaylistId = playlistId;
            await _context.Tracks.AddAsync(track);
        }
    }
}
