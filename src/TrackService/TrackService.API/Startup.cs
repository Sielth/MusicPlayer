using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using System;
using TrackService.Application.Repo;
using TrackService.Application.SyncDataServices.Http;
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
      });

      Seed.PrepPopulation(app);
    }
  }
}
