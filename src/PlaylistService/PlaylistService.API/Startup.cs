using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlaylistService.Application.EventProcessing;
using PlaylistService.Application.Repo;
using PlaylistService.Infrastructure.AsyncDataServices;
using PlaylistService.Persistence.Repo;
using Raven.Client.Documents;
using System;

namespace PlaylistService.API
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

      services.AddSingleton<IDocumentStore>(provider =>
      {
        var databaseName = "Playlist";
        //var databaseUrl = "http://localhost:8080";
        var databaseUrl = "http://172.17.0.2:8080";

        var store = new DocumentStore
        {
          Database = databaseName,
          Urls = new[] { databaseUrl }
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
    }
  }
}
