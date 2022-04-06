using System.ComponentModel.DataAnnotations;

namespace PlaylistService.Shared.DTOs.TrackDTOs
{
    public class TrackCreateDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ArtistId { get; set; }
    }
}
