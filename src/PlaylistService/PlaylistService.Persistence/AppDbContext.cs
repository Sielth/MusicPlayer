using Microsoft.EntityFrameworkCore;
using PlaylistService.Core.Entities;

namespace PlaylistService.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Playlist>()
                .HasOne(p => p.User)
                .WithMany(u => u.Playlist)
                .HasForeignKey(p => p.UserId);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Playlist)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        }
    }
}
