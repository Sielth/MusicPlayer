using System.ComponentModel.DataAnnotations;

namespace PlaylistService.Shared.DTOs.PlaylistDTOs
{
    public class CreatePlaylistDTO
    {
        [Required]
        public string Title { get; set; }
    }
}
