using MusicPlatformApi.Data.Entities;
using System.Linq.Expressions;

namespace MusicPlatformApi.Repositories
{
    public interface IMusicRepository
    {
        public Song? GetSong(int id);

        public Song? GetSongAlbum(int id);

        public IEnumerable<Author> GetAuthors(out int totalPages, int page = 1, int items = 6);

        public IEnumerable<Genre> GetGenres();

        public IEnumerable<Song> GetSongs(string searchTerm, IEnumerable<int> genreIds, out int totalPages, string orderByProperty, bool orderByDescending = true, int page = 1, int items = 6);

        public IEnumerable<PlaylistSong> GetPlaylistSongs<TProperty>(int playlistId, string searchTerm, IEnumerable<int> genreIds, Func<PlaylistSong, TProperty> orderByProperty, out int totalPages, bool orderByDescending = true, int page = 1, int items = 6);

        public IEnumerable<Playlist> GetPlaylists(string userId, out int totalPages, int page = 1, int items = 6);

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class;

        public void Remove<TEntity>(TEntity entity);

        public Task<bool> IsPlaylistNamePresent(string playlistName, string userId);

        public bool DoesPlaylistSongExist(PlaylistSong playlistSong);

        public bool DoesPlaylistBelongToUser(int playlistId, string userId);

        public bool DoesSongExist(Song song);

        public Task AddSong(Song song);

        public Task IncrementPopularity(int songId);
    }
}
