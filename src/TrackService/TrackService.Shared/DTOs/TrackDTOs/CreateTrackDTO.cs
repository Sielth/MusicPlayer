using System.ComponentModel.DataAnnotations;

namespace TrackService.Shared.DTOs.TrackDTOs
{
  public class CreateTrackDTO
  {
    [Required]
    public string Title { get; set; }

    [Required]
    public string Artist { get; set; }

    [Required]
    public string Genre { get; set; }
  }
}
