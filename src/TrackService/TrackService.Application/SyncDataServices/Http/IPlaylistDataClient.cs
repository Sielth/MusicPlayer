using System.Threading.Tasks;
using TrackService.Shared.DTOs.TrackDTOs;

namespace TrackService.Application.SyncDataServices.Http
{
  public interface IPlaylistDataClient
  {
    Task SendTrackToCommand(ReadTrackDTO track);
  }
}
