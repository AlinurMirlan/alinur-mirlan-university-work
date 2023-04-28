using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatformApi.Data.Entities
{
    [Index(nameof(Nickname))]
    public class Author
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string Nickname { get; set; }

        public List<Song> Songs { get; set; } = new();
    }
}
