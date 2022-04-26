using PlaylistService.Core.Entities;
using System.Collections.Generic;

namespace PlaylistService.Application.PlaylistLogic.CQRS.Responses
{
  public class PlaylistResponse
  {
    public string Id { get; set; }

    public string Title { get; set; }

    public ICollection<Track> Tracks { get; set; }

    public int UserId { get; set; }
  }
}
