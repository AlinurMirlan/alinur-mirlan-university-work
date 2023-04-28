using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatformApi.Models
{
    public class AlbumDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}
