using MoodService.App.Models;

namespace MoodService.App.DTOs
{
  public class TrackAnalyzedDTO
  {
    public string Id { get; set; }
    public string Mood { get; set; }
    public string Event { get; set; }
  }
}
