using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlaylistService.Application.EventProcessing;
using PlaylistService.Application.Repo;
using PlaylistService.Application.SyncDataServices.Grpc;
using PlaylistService.Infrastructure.AsyncDataServices;
using PlaylistService.Persistence.Repo;
using Raven.Client.Documents;
using System;
using System.Security.Cryptography.X509Certificates;

namespace PlaylistService.API
{
  public class Startup
  {
    X509Certificate2 clientCertificate = new X509Certificate2("../../../free.sielth.client.certificate/free.sielth.client.certificate.pfx");

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddSingleton<IDocumentStore>(provider =>
      {
        var databaseName = "Playlist";
        var databaseUrl = "https://a.free.sielth.ravendb.cloud";

        var store = new DocumentStore
        {
          Database = databaseName,
          Urls = new[] { databaseUrl },
          Certificate = clientCertificate
        };
        store.Initialize();
        return store;
      });

      services.AddScoped<IPlaylistRepo, PlaylistRepo>();
      services.AddScoped<ITrackRepo, TrackRepo>();

      services.AddSingleton<IEventProcessor, EventProcessor>();
      services.AddHostedService<MessageBusSubscriber>();

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

      services.AddScoped<ITrackDataClient, TrackDataClient>();

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlaylistService.API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlaylistService.API v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      Seed.PrepPopulation(app);
    }
  }
}
