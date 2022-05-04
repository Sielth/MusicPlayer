using MoodService.App.Models;
using System.Threading.Tasks;

namespace MoodService.App.Services
{
  public interface ISpotifyService
  {
    Task<TrackFeatures> Run(Track track);
  }
}