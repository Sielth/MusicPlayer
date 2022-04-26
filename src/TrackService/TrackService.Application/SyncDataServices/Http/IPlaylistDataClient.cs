using System.Threading.Tasks;
using TrackService.Shared.DTOs.TrackDTOs;

namespace TrackService.Application.SyncDataServices.Http
{
  public interface IPlaylistDataClient
  {
    Task SendTrackToPlaylist(ReadTrackDTO track);
  }
}
