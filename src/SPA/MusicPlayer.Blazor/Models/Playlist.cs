using System.Collections.Generic;

namespace MusicPlayer.Blazor.Models
{
  public class Playlist
  {
    public string Id { get; set; }

    public string Title { get; set; }

    public ICollection<Track> Tracks { get; set; }
  }
}
