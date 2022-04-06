namespace PlaylistService.Shared.DTOs.TrackDTOs
{
    public class TrackReadDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ArtistId { get; set; }

        public int PlaylistId { get; set; }
    }
}
