using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackService.Application.Repo;
using TrackService.Core.Entities;

namespace TrackService.Persistence.Repo
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

    public async Task<IEnumerable<Track>> GetTracksByMood(string mood)
    {
      using (var session = _documentStore.OpenAsyncSession())
      {
        var tracks = await session.Query<Track>()
          .Where(t => t.Mood == mood)
          .ToListAsync();
        return tracks;
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

    public async Task UpdateMoodOfTrack(Track track)
    {
      using (var session = _documentStore.OpenAsyncSession())
      {
        var t = await session.LoadAsync<Track>(track.Id);
        t.Mood = track.Mood;
        await session.SaveChangesAsync();
      }
    }
  }
}
