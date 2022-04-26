using System.ComponentModel.DataAnnotations;

namespace PlaylistService.Core.Entities
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
  }
}
