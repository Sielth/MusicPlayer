using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlaylistService.Core.Entities
{
    public class Playlist
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        //public ICollection<Track> Tracks { get; set; }
    }
}
