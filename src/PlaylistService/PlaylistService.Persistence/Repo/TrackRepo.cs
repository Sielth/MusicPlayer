using PlaylistService.Application.Repo;
using PlaylistService.Core.Entities;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaylistService.Persistence.Repo
{
  public class TrackRepo : ITrackRepo
  {
    private readonly IDocumentStore _documentStore;

    public TrackRepo(IDocumentStore documentStore)
    {
      _documentStore = documentStore;
    }

    public async Task CreateTrack(Track track)
    {
      if (track == null)
      {
        throw new ArgumentNullException(nameof(track));
      }

      using (var session = _documentStore.OpenAsyncSession())
      {
        await session.StoreAsync(track);
        await session.SaveChangesAsync();
      }
    }

    public async Task<IEnumerable<Track>> GetTracks()
    {
      using (var session = _documentStore.OpenAsyncSession())
      {
        var tracks = await session.Query<Track>().ToListAsync();
        return tracks;
      }
    }

    public async Task<bool> TrackExists(string trackId)
    {
      using (var session = _documentStore.OpenAsyncSession())
      {
        return await session.Advanced.ExistsAsync(trackId);
      }
    }

    public async Task<Track> GetTrack(string trackId)
    {
      using (var session = _documentStore.OpenAsyncSession())
      {
        var track = await session.LoadAsync<Track>(trackId);
        return track;
      }
    }
  }
}
