using Microsoft.Extensions.Configuration;
using MoodService.App.Models;
using SpotifyAPI.Web;
using System.Threading.Tasks;

namespace MoodService.App.Services
{
  public class SpotifyService : ISpotifyService
  {
    private readonly IConfiguration _config;

    public SpotifyService(IConfiguration config)
    {
      _config = config;
    }

    public async Task<TrackFeatures> Run(Track track)
    {
      // TODO: Deploy secrets on K8S 
      //var clientId = _config["CLIENT_ID"];
      //var clientSecret = _config["CLIENT_SECRET"];

      var clientId = "290f525281a54140971244f79ee55889";
      var clientSecret = "c8a1f0c67107413aa902ef9e0ccd1abb";

      var opt = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator(clientId, clientSecret));
      var spotify = new SpotifyClient(opt);

      var response = await spotify.Search.Item(new SearchRequest(SearchRequest.Types.Track, $"{track.Title} {track.Artist}"));

      var id = "";

      await foreach (var item in spotify.Paginate(response.Tracks, (s) => s.Tracks))
      {
        id = item.Id;
        break;
      }

      var features = await spotify.Tracks.GetAudioFeatures(id);

      return new TrackFeatures
      {
        Danceability = features.Danceability,
        Acousticness = features.Acousticness,
        Energy = features.Energy,
        Instrumentalness = features.Instrumentalness,
        Valence = features.Valence,
        Loudness = features.Loudness,
        Speechiness = features.Speechiness
      };
    }
  }
}
