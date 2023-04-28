using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatformApi.Data.Entities
{
    public class User : IdentityUser
    {
        public int Age { get; set; }

        [Column(TypeName = "char(1)")]
        public required string Sex { get; set; }

        public required string Name { get; set; }

        public List<Playlist> Playlists { get; set; } = new();
    }
}
