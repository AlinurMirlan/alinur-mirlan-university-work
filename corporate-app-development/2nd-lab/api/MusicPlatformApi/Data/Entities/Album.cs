using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatformApi.Data.Entities
{
    public class Album
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string Name { get; set; }

        public List<Song> Songs { get; set; } = new List<Song>(); 
    }
}
