using PlaylistService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlaylistService.Persistence.Repo
{
    public class PlaylistRepo : IPlaylistRepo //TODO: PrepDb class when grpcClient is done
    {
        private readonly AppDbContext _context;

        public PlaylistRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlaylist(int userId, Playlist playlist)
        {
            if (playlist == null)
            {
                throw new ArgumentNullException(nameof(playlist));
            }

            playlist.UserId = userId;
            _context.Playlists.Add(playlist);
        }

        public IEnumerable<Playlist> GetPlaylistsForUser(int userId)
        {
            return _context.Playlists
                .Where(p => p.UserId == userId)
                .OrderBy(p => p.Title);
        }

        public Playlist GetPlaylist(int userId, int playlistId)
        {
            return _context.Playlists
                .Where(p => p.UserId == userId && p.Id == playlistId)
                .FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public bool ExternalUserExists(int externalUserId)
        {
            return _context.Users.Any(u => u.ExternalId == externalUserId);
        }
    }
}
