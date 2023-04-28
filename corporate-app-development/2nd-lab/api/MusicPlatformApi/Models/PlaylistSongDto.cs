using MusicPlatformApi.Data.Entities;

namespace MusicPlatformApi.Models
{
    public class PlaylistSongDto
    {
        public int SongId { get; set; }

        public int PlaylistId { get; set; }

        public required SongDto Song { get; set; }

        public DateTime DateTimeAdded { get; set; }
    }
}
