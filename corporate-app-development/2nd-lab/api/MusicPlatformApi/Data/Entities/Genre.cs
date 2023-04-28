using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatformApi.Data.Entities
{
    [PrimaryKey(nameof(Id))]
    public class Genre
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public required string Name { get; set; }
    }
}
