using CsvHelper;
using CsvHelper.Configuration;
using MusicPlatformApi.Data.Entities;
using System.Globalization;

namespace MusicPlatformApi.Infrastructure
{
    public class SongReader
    {
        private readonly string _csvFilePath;

        public SongReader(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public Song[] ReadSongs()
        {
            FileStream fileStream = File.OpenRead(_csvFilePath);
            CsvConfiguration config = new CsvConfiguration(CultureInfo.CurrentCulture) { Delimiter= ";" };
            CsvReader csvReader = new(new StreamReader(fileStream), config);
        
            // Skipping Headers
            csvReader.Read();
            csvReader.ReadHeader();

            LinkedList<Song> songs = new();
            Dictionary<string, Author> authors = new();
            Dictionary<string, Genre> genres = new();
            Dictionary<string, Album> albums = new();
            while (csvReader.Read())
            {
                Song song = new()
                {
                    Title = string.IsNullOrEmpty(csvReader[0])
                        ? throw new InvalidOperationException($"{nameof(Song.Title)} cannot be empty.")
                        : csvReader[0]!.Trim(),
                    ImageFile = string.IsNullOrEmpty(csvReader[3])
                        ? throw new InvalidOperationException($"{nameof(Song.ImageFile)} cannot be empty.")
                        : csvReader[3]!.Trim(),
                    SongFile = string.IsNullOrEmpty(csvReader[4])
                        ? throw new InvalidOperationException($"{nameof(Song.SongFile)} cannot be empty.")
                        : csvReader[4]!.Trim(),
                    ReleaseDate = Convert.ToDateTime(
                        string.IsNullOrEmpty(csvReader[6])
                        ? throw new InvalidOperationException($"{nameof(Song.ReleaseDate)} cannot be empty.")
                        : csvReader[6]!.Trim()),
                    Popularity = string.IsNullOrEmpty(csvReader[7]) ? 0 : int.Parse(csvReader[7]!) 
                };

                if (string.IsNullOrEmpty(csvReader[1]))
                    throw new InvalidOperationException($"{nameof(Song.Authors)} cannot be empty.");

                if (string.IsNullOrEmpty(csvReader[5]))
                    throw new InvalidOperationException($"{nameof(Song.Genres)} cannot be empty.");

                string? albumName = csvReader[2];
                if (albumName is not null)
                {
                    if (!albums.ContainsKey(albumName))
                    {
                        Album album = new() { Name = albumName };
                        albums.Add(albumName, album);
                    }
                    song.Album = albums[albumName];
                }

                string[] authorNames = csvReader[1]!.Split(',');
                song.Authors = new List<Author>(authorNames.Length);
                for (int i = 0; i < authorNames.Length; i++)
                {
                    string authorName = authorNames[i].Trim();
                    Author author;
                    if (authors.ContainsKey(authorName))
                    {
                        author = authors[authorName];
                    }
                    else
                    {
                        author = new Author { Nickname = authorName };
                        authors.Add(authorName, author);
                    }

                    song.Authors.Add(author);
                }

                string[] songGenres = csvReader[5]!.Split(',');
                song.Genres = new List<Genre>(songGenres.Length);
                for (int i = 0; i < songGenres.Length; i++)
                {
                    string genreName = songGenres[i].Trim();
                    Genre genre;
                    if (genres.ContainsKey(genreName))
                    {
                        genre = genres[genreName];
                    }
                    else
                    {
                        genre = new Genre { Name = genreName };
                        genres.Add(genreName, genre);
                    }

                    song.Genres.Add(genre);
                }

                songs.AddLast(song);
            }

            return songs.ToArray();
        }
    }
}
