using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using System;
using System.IO;
using TrackService.Application.AsyncDataServices;
using TrackService.Application.EventProcessing;
using TrackService.Application.Repo;
using TrackService.Application.SyncDataServices.Grpc;
using TrackService.Application.SyncDataServices.Http;
using TrackService.Infrastructure.AsyncDataServices;
using TrackService.Infrastructure.SyncDataServices.Http;
using TrackService.Persistence.Repo;

namespace TrackService.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackService.API", Version = "v1" });
      });

      services.AddSingleton<IDocumentStore>(provider =>
      {
        var databaseName = "Track";
        //var databaseUrl = "http://localhost:8080"; //localhost - for testing purposes
        var databaseUrl = "http://172.17.0.2:8080";

        var store = new DocumentStore
        {
          Database = databaseName,
          Urls = new[] { databaseUrl }
        };
        store.Initialize();
        return store;
      });

      services.AddScoped<ITrackRepo, TrackRepo>();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
      services.AddHttpClient<IPlaylistDataClient, HttpPlaylistDataClient>();

      services.AddSingleton<IMessageBusClient, MessageBusClient>();
      services.AddSingleton<IEventProcessor, EventProcessor>();
      services.AddHostedService<MessageBusSubscriber>();

      services.AddGrpc();

      Console.WriteLine($"--> PlaylistService Endpoint {Configuration["PlaylistService"]}");
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackService.API v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapGrpcService<GrpcTrackService>();

        endpoints.MapGet("../TrackService.Application/protos/tracks.proto", async context =>
        {
          await context.Response.WriteAsync(File.ReadAllText("../TrackService.Application/Protos/tracks.proto"));
        });
      });

      Seed.PrepPopulation(app);
    }
  }
}
