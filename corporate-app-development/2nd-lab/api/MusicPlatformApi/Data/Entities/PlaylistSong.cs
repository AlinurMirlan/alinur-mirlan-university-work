using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatformApi.Data.Entities
{
    [PrimaryKey(nameof(SongId), nameof(PlaylistId))]
    public class PlaylistSong
    {
        public int SongId { get; set; }

        public int PlaylistId { get; set; }

        public Song? Song { get; set; }

        public Playlist? Playlist { get; set; }

        public DateTime DateTimeAdded { get; set; } = DateTime.Now;
    }
}
