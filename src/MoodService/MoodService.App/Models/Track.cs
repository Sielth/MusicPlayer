namespace MoodService.App.Models
{
  public class Track
  {
    public string Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public MoodType Mood { get; set; }
  }
}
