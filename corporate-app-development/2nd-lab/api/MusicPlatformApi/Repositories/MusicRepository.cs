using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using MusicPlatformApi.Data;
using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Infrastructure;
using System.Linq.Expressions;

namespace MusicPlatformApi.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        private readonly MusicContext _context;

        public MusicRepository(MusicContext context)
        {
            _context = context;
        }

        public IEnumerable<Song> GetSongs<TKey>(Func<Song, TKey> propertySelector, out int totalPages, bool orderByAscending = true, int page = 1, int items = 6)
        {
            IQueryable<Song> songs = _context.Songs.AsNoTracking()
                            .Include(song => song.Authors)
                            .Include(song => song.Genres);

            IOrderedEnumerable<Song> orderedSongs = orderByAscending ? songs.OrderBy(propertySelector) : songs.OrderByDescending(propertySelector);

            totalPages = (int)Math.Ceiling((double)orderedSongs.Count() / items);
            return orderedSongs
                .Skip((page - 1) * items)
                .Take(items);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.AsEnumerable();
        }

        public Song? GetSong(int id)
        {
            return _context.Songs.Find(id);
        }

        public Song? GetSongAlbum(int id)
        {
            return _context.Songs.Include(song => song.Album).FirstOrDefault(song => song.Id == id);
        }

        public IEnumerable<Song> GetSongs(string searchTerm, IEnumerable<int> genreIds, out int totalPages, string orderByProperty, bool orderByDescending = true, int page = 1, int items = 6)
        {
            string[] tokens = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            IQueryable<Song> songs = _context.Songs
                            .Include(song => song.Authors)
                            .Include(song => song.Genres)
                            .Include(song => song.Album);

            foreach (string token in tokens)
                songs = songs.Where(song => song.Signature!.Contains(token));

            foreach (int genreId in genreIds)
                songs = songs.Where(song => song.Genres.Any(genre => genre.Id == genreId));

            IOrderedQueryable<Song> orderedSongs = songs.OrderBy(orderByProperty, orderByDescending);
            totalPages = (int)Math.Ceiling((double)orderedSongs.Count() / items);
            return orderedSongs
                .Skip((page - 1) * items)
                .Take(items);
        }

        public IEnumerable<PlaylistSong> GetPlaylistSongs<TProperty>(int playlistId, string searchTerm, IEnumerable<int> genreIds, Func<PlaylistSong, TProperty> orderByProperty, out int totalPages, bool orderByDescending = true, int page = 1, int items = 6)
        {
            string[] tokens = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Playlist playlist = _context.Playlists
                            .Include(playlist => playlist.Songs)
                                .ThenInclude(playlist => playlist.Song)
                                    .ThenInclude(song => song!.Genres)
                            .Include(playlist => playlist.Songs)
                                .ThenInclude(playlist => playlist.Song)
                                    .ThenInclude(song => song!.Album)
                            .Include(playlist => playlist.Songs)
                                .ThenInclude(playlist => playlist.Song)
                                    .ThenInclude(song => song!.Authors)
                            .Where(playlist => playlist.Id == playlistId)
                            .First();

            IEnumerable<PlaylistSong> songs;
            songs = playlist.Songs.Where(playlistSong =>
                tokens.All(token => playlistSong.Song!.Signature!.Contains(token, StringComparison.InvariantCultureIgnoreCase)) &&
                genreIds.All(genreId => playlistSong.Song!.Genres.Any(genre => genre.Id == genreId)));

            IOrderedEnumerable<PlaylistSong> orderedSongs;
            if (orderByDescending)
                orderedSongs = songs.OrderByDescending(orderByProperty);
            else
                orderedSongs = songs.OrderBy(orderByProperty);

            totalPages = (int)Math.Ceiling((double)orderedSongs.Count() / items);
            return orderedSongs
                .Skip((page - 1) * items)
                .Take(items);
        }

        public IEnumerable<Playlist> GetPlaylists(string userId, out int totalPages, int page = 1, int items = 6)
        {
            IQueryable<Playlist> playlists = _context.Playlists.Where(playlist => playlist.UserId == userId);
            totalPages = (int)Math.Ceiling((double)playlists.Count() / items);
            return playlists
                .Skip((page - 1) * items)
                .Take(items);
        }

        public bool DoesPlaylistSongExist(PlaylistSong playlistSong)
        {
            PlaylistSong? song = _context.PlaylistSongs.Find(playlistSong.SongId, playlistSong.PlaylistId);
            return song is not null;
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            EntityEntry<TEntity> addedEntity = _context.Add(entity);
            _context.SaveChanges();
            return addedEntity.Entity;
        }

        public void Remove<TEntity>(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _context.ChangeTracker.Clear();
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<bool> IsPlaylistNamePresent(string playlistName, string userId)
        {
            Playlist? playlist = await _context.Playlists.Where(playlist => playlist.Name == playlistName && playlist.UserId == userId).FirstOrDefaultAsync();
            return playlist is not null;
        }

        public bool DoesPlaylistBelongToUser(int playlistId, string userId)
        {
            Playlist? playlist = _context.Playlists.FirstOrDefault(playlist => playlist.Id == playlistId && playlist.UserId == userId);
            return playlist is not null;
        }

        public bool DoesSongExist(Song song)
        {
            IQueryable<Song> songs = _context.Songs.Where(s => s.Title == song.Title.Trim());
            foreach (Author author in song.Authors)
                songs = songs.Where(song => song.Authors.Any(a => a.Nickname == author.Nickname.Trim()));
            return songs.FirstOrDefault() is not null;
        }

        public async Task AddSong(Song song)
        {
            if (song.Album is not null)
            {
                song.Album.Name = song.Album.Name.Trim();
                Album? album = _context.Albums.FirstOrDefault(a => a.Name == song.Album.Name);
                if (album is not null)
                    song.Album = album;
            }

            for (int i = 0; i < song.Authors.Count; i++)
            {
                song.Authors[i].Nickname = song.Authors[i].Nickname.Trim();
                Author? songAuthor = _context.Authors.FirstOrDefault(a => a.Nickname == song.Authors[i].Nickname);
                if (songAuthor is null)
                    continue;

                song.Authors[i] = songAuthor;
            }

            for (int i = 0; i < song.Genres.Count; i++)
            {
                song.Genres[i].Name = song.Genres[i].Name.Trim();
                Genre? songGenre = _context.Genres.FirstOrDefault(g => g.Name == song.Genres[i].Name);
                if (songGenre is null)
                    continue;

                song.Genres[i] = songGenre;
            }

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
        }

        public async Task IncrementPopularity(int songId)
        {
            Song? song = _context.Songs.Find(songId);
            if (song is null)
                throw new ArgumentException($"There is no song with the given id {songId}");
            song.Popularity++;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Author> GetAuthors(out int totalPages, int page = 1, int items = 6)
        {
            IQueryable<Author> authors = _context.Authors;
            totalPages = (int)Math.Ceiling((double)authors.Count() / items);
            return authors
                .Skip((page - 1) * items)
                .Take(items);
        }
    }
}
