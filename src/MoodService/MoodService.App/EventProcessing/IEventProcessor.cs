using System.Threading.Tasks;

namespace MoodService.App.EventProcessing
{
  public interface IEventProcessor
  {
    Task ProcessEvent(string message);
  }
}
