using MoodService.App.DTOs;

namespace MoodService.App.AsyncDataServices
{
  public interface IMessageBusClient
  {
    void PublishTrackAnalyzed(TrackAnalyzedDTO trackAnalyzedDTO);
  }
}
