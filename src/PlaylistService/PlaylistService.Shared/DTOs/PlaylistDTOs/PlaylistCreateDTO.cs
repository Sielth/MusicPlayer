using System.ComponentModel.DataAnnotations;

namespace PlaylistService.Shared.DTOs.PlaylistDTOs
{
    public class PlaylistCreateDTO
    {
        [Required]
        public string Title { get; set; }
    }
}
