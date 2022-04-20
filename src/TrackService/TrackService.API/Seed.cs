using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using TrackService.Persistence.Data;

namespace TrackService.API
{
  public static class Seed
  {
    public async static void PrepPopulation(IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {
        await PrepDb.SeedData(serviceScope.ServiceProvider.GetService<IDocumentStore>());
      }
    }
  }
}
