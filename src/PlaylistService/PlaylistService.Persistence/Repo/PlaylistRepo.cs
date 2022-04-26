using PlaylistService.Application.Repo;
using PlaylistService.Core.Entities;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaylistService.Persistence.Repo
{
  public class PlaylistRepo : IPlaylistRepo
  {
    private readonly IDocumentStore _documentStore;

    public PlaylistRepo(IDocumentStore documentStore)
    {
      _documentStore = documentStore;
    }

    public async Task CreatePlaylist(int userId, Playlist playlist)
    {
      if (playlist == null)
      {
        throw new ArgumentNullException(nameof(playlist));
      }

      playlist.UserId = userId;

      using (var session = _documentStore.OpenAsyncSession())
      {
        await session.StoreAsync(playlist);
        await session.SaveChangesAsync();
      }
    }

    public async Task<IEnumerable<Playlist>> GetPlaylistsForUser(int userId)
    {
      using (var session = _documentStore.OpenAsyncSession())
      {
        var tracks = await session.Query<Playlist>()
          .Where(p => p.UserId == userId)
          .OrderBy(p => p.Title)
          .ToListAsync();
        return tracks;
      }
    }

    public async Task<Playlist> GetPlaylistForUser(int userId, string playlistId)
    {
      using (var session = _documentStore.OpenAsyncSession())
      {
        return await session.Query<Playlist>()
          .Where(p => p.UserId == userId && p.Id == playlistId)
          .FirstOrDefaultAsync();
      }
    }

    public async Task AddTrackToPlaylist(int userId, string playlistId, Track track)
    {
      using (var session = _documentStore.OpenAsyncSession())
      {
        var playlist = await session
          .Query<Playlist>()
          .Where(p => p.UserId == userId && p.Id == playlistId)
          .Include(p => p.Tracks)
          .FirstOrDefaultAsync();

        if (playlist.Tracks == null)
        {
          playlist.Tracks = new List<Track>();
          playlist.Tracks.Add(track);
        }
        else
        {
          playlist.Tracks.Add(track);
        }

        await session.SaveChangesAsync();
      }
    }

    // TODO: Move it when User Service is running
    // User

    //public async Task CreateUser(User user)
    //{
    //  if (user == null)
    //  {
    //    throw new ArgumentNullException(nameof(user));
    //  }

    //  await _context.Users.AddAsync(user);
    //}

    //public async Task<bool> UserExists(int userId)
    //{
    //  return await _context.Users.AnyAsync(u => u.Id == userId);
    //}

    //public async Task<bool> ExternalUserExists(int externalUserId)
    //{
    //  return await _context.Users.AnyAsync(u => u.ExternalId == externalUserId);
    //}
  }
}
