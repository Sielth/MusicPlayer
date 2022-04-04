using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlaylistService.Core.Entities
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalId { get; set; }

        [Required]
        public string UserName { get; set; }

        public ICollection<Playlist> Playlist { get; set; } = new List<Playlist>();
    }
}
