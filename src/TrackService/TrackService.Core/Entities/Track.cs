using System.ComponentModel.DataAnnotations;

namespace TrackService.Core.Entities
{
  public class Track
  {
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    public string Title { get; set; }

    //[Required]
    //public string ArtistId { get; set; }

    [Required]
    public string Artist { get; set; }

    [Required]
    public string Genre { get; set; }

    public string Mood { get; set; }
  }
}
