namespace PlaylistService.Application.EventProcessing
{
  public interface IEventProcessor
  {
    void ProcessEvent(string message);
  }
}
