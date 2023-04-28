using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Infrastructure;

namespace MusicPlatformApi.Data
{
    public class MusicContext : IdentityDbContext<User>
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<PlaylistSong> PlaylistSongs { get; set; }

        public DbSet<Album> Albums { get; set; }

        public MusicContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Song>()
                .HasMany(song => song.Authors)
                .WithMany(author => author.Songs);

            builder.Entity<Song>()
                .HasMany(song => song.Genres)
                .WithMany();

            builder.Entity<Song>()
                .HasOne(song => song.Album)
                .WithMany(album => album.Songs);
        }
    }
}
