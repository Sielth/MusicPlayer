using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlaylistService.Application.Repo;
using PlaylistService.Application.SyncDataServices.Grpc;
using PlaylistService.Persistence.Data;
using Raven.Client.Documents;

namespace PlaylistService.API
{
  public static class Seed
  {
    public async static void PrepPopulation(IApplicationBuilder app)
    {
      using (var serviceScope = app.ApplicationServices.CreateScope())
      {
        var grpcClient = serviceScope.ServiceProvider.GetService<ITrackDataClient>();

        var tracks = grpcClient.ReturnAllTracks();

        await PrepDb.SeedData(
          serviceScope.ServiceProvider.GetService<IDocumentStore>(),
          tracks);
      }
    }
  }
}
