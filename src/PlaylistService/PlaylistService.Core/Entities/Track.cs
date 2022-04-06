using System.ComponentModel.DataAnnotations;

namespace PlaylistService.Core.Entities
{
    public class Track
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ArtistId { get; set; }

        [Required]
        public int PlaylistId { get; set; }

        public Playlist Playlist { get; set; }
    }
}
