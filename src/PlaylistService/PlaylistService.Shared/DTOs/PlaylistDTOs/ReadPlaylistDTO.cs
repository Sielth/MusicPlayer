using PlaylistService.Shared.DTOs.TrackDTOs;
using System.Collections.Generic;

namespace PlaylistService.Shared.DTOs.PlaylistDTOs
{
  public class ReadPlaylistDTO
  {
    public string Id { get; set; }

    public string Title { get; set; }

    public int UserId { get; set; }

    public ICollection<ReadTrackDTO> Tracks { get; set; }
  }
}
