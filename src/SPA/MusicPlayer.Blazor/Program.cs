using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MusicPlayer.Blazor.Data;
using Radzen;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Blazor
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");

      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

      builder.Services.AddScoped<DialogService>();
      builder.Services.AddScoped<NotificationService>();
      builder.Services.AddScoped<TooltipService>();
      builder.Services.AddScoped<ContextMenuService>();

      builder.Configuration.AddJsonFile("appsettings.json", true, false);

      builder.Configuration.AddEnvironmentVariables();

      var uri = builder.Configuration.GetValue<string>("TrackService");
      var trackService = RestService.For<ITrackService>(new HttpClient
      {
        BaseAddress = new Uri(uri)
      });

      var playlistService = RestService.For<IPlaylistService>(new HttpClient
      {
        BaseAddress = new Uri(uri)
      });

      builder.Services.AddSingleton(trackService);
      builder.Services.AddSingleton(playlistService);

      await builder.Build().RunAsync();
    }
  }
}
