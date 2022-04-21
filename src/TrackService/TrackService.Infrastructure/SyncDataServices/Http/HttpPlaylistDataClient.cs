using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrackService.Application.SyncDataServices.Http;
using TrackService.Shared.DTOs.TrackDTOs;

namespace TrackService.Infrastructure.SyncDataServices.Http
{
  public class HttpPlaylistDataClient : IPlaylistDataClient
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpPlaylistDataClient(HttpClient httpClient, IConfiguration configuration)
    {
      _httpClient = httpClient;
      _configuration = configuration;
    }

    public async Task SendTrackToCommand(ReadTrackDTO track)
    {
      var httpContent = new StringContent(
                      JsonSerializer.Serialize(track),
                      Encoding.UTF8,
                      "application/json");

      var response = await _httpClient.PostAsync($"{_configuration["PlaylistService"]}", httpContent);

      if (response.IsSuccessStatusCode)
      {
        Console.WriteLine("--> Sync POST to PlaylistService was OK!");
      }
      else
      {
        Console.WriteLine("--> Sync POST to PlaylistService was NOT OK!");
      }
    }
  }
}
