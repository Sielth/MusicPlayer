using PlaylistService.Core.Entities;
using PlaylistService.Persistence.Repo;
using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaylistService.Persistence.Data
{
  public static class PrepDb
  {
    // TODO: AppDbContext? bool isProd
    public async static Task SeedData(IDocumentStore store, IEnumerable<Track> tracks)
    {
      using (var session = store.OpenAsyncSession())
      {
        Console.WriteLine("Seeding new tracks...");

        foreach (var track in tracks)
        {
          if (! await session.Advanced.ExistsAsync(track.Id))
          {
            await session.StoreAsync(track);
            await session.SaveChangesAsync();
          }
        }
      }
    }
  }
}
