using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicPlatformApi.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatformApi.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }

        public required string Nickname { get; set; }
    }
}
