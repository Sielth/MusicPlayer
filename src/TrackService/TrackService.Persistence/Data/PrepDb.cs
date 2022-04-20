using Raven.Client.Documents;
using System;
using System.Threading.Tasks;
using TrackService.Core.Entities;

namespace TrackService.Persistence.Data
{
  public static class PrepDb
  {
    // TODO: AppDbContext? bool isProd
    public async static Task SeedData(IDocumentStore store)
    {
      using (var session = store.OpenAsyncSession())
      {
        if (!await session.Query<Track>().AnyAsync())
        {
          Console.WriteLine("--> Seeding Data...");

          await session.StoreAsync(
            new Track
            {
              Title = "Ok Computer",
              Artist = "Radiohead",
              Genre = "Rock"
            }
          );

          await session.StoreAsync(
            new Track
            {
              Title = "Viva La Vida",
              Artist = "Coldplay",
              Genre = "Rock"
            }
          );

          await session.StoreAsync(
            new Track
            {
              Title = "Song 2",
              Artist = "Blur",
              Genre = "Britpop"
            }
          );

          await session.SaveChangesAsync();
        }
        else
        {
          Console.WriteLine("--> We already have data.");
        }
      }
    }
  }
}
