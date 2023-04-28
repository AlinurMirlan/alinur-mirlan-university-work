namespace MusicPlatformApi.Data.Entities
{
    public class Playlist
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string UserId { get; set; }

        public User? User { get; set; }

        public List<PlaylistSong> Songs { get; set; } = new();

        public DateTime DateTimeAdded { get; set; } = DateTime.Now;
    }
}
