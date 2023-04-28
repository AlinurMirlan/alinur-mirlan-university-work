using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicPlatformApi.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicPlatformApi.Models
{
    public class SongDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public List<AuthorDto> Authors { get; set; } = new();

        public AlbumDto? Album { get; set; }

        public List<Genre> Genres { get; set; } = new();

        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        public int Popularity { get; set; }
    }
}
