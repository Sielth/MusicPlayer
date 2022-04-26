using System.Threading.Tasks;
using TrackService.Shared.DTOs.TrackDTOs;

namespace TrackService.Application.AsyncDataServices
{
  public interface IMessageBusClient
  {
    void PublishNewTrack(PublishedTrackDTO publishedTrackDTO);
  }
}
