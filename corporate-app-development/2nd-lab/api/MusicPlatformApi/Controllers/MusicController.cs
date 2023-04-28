using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Models;
using MusicPlatformApi.Repositories;
using MusicPlatformApi.Services;
using System;
using System.Linq.Expressions;

namespace MusicPlatformApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly IMusicRepository _musicRepo;
        private readonly IMapper _mapper;
        private readonly FileHandler _fileHandler;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public MusicController(UserManager<User> userManager, IMusicRepository musicRepo, IMapper mapper, IConfiguration config, FileHandler fileHandler)
        {
            _musicRepo = musicRepo;
            _userManager = userManager;
            _mapper = mapper;
            _fileHandler = fileHandler;
            _config = config;
        }


        [HttpGet("song/{songId}")]
        public IActionResult GetSong([FromRoute]int songId)
        {
            Song? song = _musicRepo.GetSong(songId);
            if (song is null)
                return NotFound();

            string songFolder = _config["Folder:Songs"] ?? throw new InvalidOperationException("Folder for songs is not set.");
            string songPath = Path.Combine(songFolder, song.SongFile);
            if (!System.IO.File.Exists(songPath))
                return NotFound();

            byte[] songRaw = System.IO.File.ReadAllBytes(songPath);
            return File(songRaw, "audio/mpeg");
        }

        [HttpGet("image/{songId}")]
        public IActionResult GetImage([FromRoute]int songId)
        {
            Song? song = _musicRepo.GetSong(songId);
            if (song is null)
                return NotFound();

            string songImageFolder = _config["Folder:SongImages"] ?? throw new InvalidOperationException("Folder for song images is not set.");
            string songImagePath = Path.Combine(songImageFolder, song.ImageFile);
            if (!System.IO.File.Exists(songImagePath))
                return NotFound();

            string fileExtension = songImagePath[^3..^1] == "ng" ? "png" : "jpeg";
            byte[] image = System.IO.File.ReadAllBytes(songImagePath);
            return File(image, $"image/{fileExtension}");
        }

        [HttpGet("{searchTerm?}")]
        public ActionResult<IEnumerable<SongDto>> GetSongs([FromQuery]int[] genreIds, [FromRoute]string searchTerm = "", int page = 1, int pageSize = 6, string orderByProperty = nameof(Song.ReleaseDate), bool orderByDescending = true)
        {
            IEnumerable<Song> songs;
            int totalPages;
            try
            {
                songs = _musicRepo.GetSongs(searchTerm, genreIds, out totalPages, orderByProperty, orderByDescending, page, pageSize);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }

            LinkedList<SongDto> songDtos = new();
            foreach (Song song in songs)
            {
                SongDto songDto = _mapper.Map<SongDto>(song);
                songDtos.AddLast(songDto);
            }

            return Ok(new PageModel<SongDto>()
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                Results = songDtos
            });
        }

        [HttpPost("song/edit/{songId:int}")]
        public IActionResult EditSong([FromRoute]int songId, Song editedSong)
        {
            Song? song = _musicRepo.GetSongAlbum(songId);
            if (song is null)
                return BadRequest($"There is no song with the give id {songId}");

            if (song.Title != editedSong.Title && song.Album!.Name != editedSong.Album?.Name && _musicRepo.DoesSongExist(song))
                return BadRequest("Given song already exsists.");

            _musicRepo.Remove(song);

            editedSong.ImageFile = string.IsNullOrWhiteSpace(editedSong.ImageFile) ? song.ImageFile : editedSong.ImageFile;
            if (!string.IsNullOrWhiteSpace(editedSong.SongFile))
                _fileHandler.DeleteSong(song.SongFile);
            else
                editedSong.SongFile = song.SongFile;

            editedSong.Popularity = song.Popularity;
            editedSong.ReleaseDate = song.ReleaseDate;
            _musicRepo.AddSong(editedSong);
            return Ok();
        }

        [HttpPost("song/{songId:int}/increment/popularity")]
        public async Task<IActionResult> IncrementPopularity(int songId)
        {
            await _musicRepo.IncrementPopularity(songId);
            return Ok();
        }

        [HttpPost("song/delete")]
        public IActionResult DeleteSong(int songId)
        {
            Song? song = _musicRepo.GetSong(songId);
            if (song is null)
                return BadRequest("There is no song with the given id");

            _musicRepo.Remove(song);
            _fileHandler.DeleteSong(song.SongFile);
            _fileHandler.DeleteImage(song.ImageFile);
            return Ok();
        }

        [HttpPost("song/add")]
        public async Task<IActionResult> AddSong(Song song)
        {
            if (_musicRepo.DoesSongExist(song))
                return BadRequest("Given song already exsists.");
    
            await _musicRepo.AddSong(song);
            return Ok();
        }

        [HttpPost("song/add/assets")]
        public async Task<IActionResult> AddSongAssets(IFormFile? songFile, IFormFile? imageFile)
        {
            if (imageFile is not null)
                await _fileHandler.UploadImage(imageFile);
            if (songFile is not null)
                await _fileHandler.UploadSong(songFile);
            return Ok();
        }


        [HttpGet("playlist/{playlistId:int}/{searchTerm?}")]
        public ActionResult<IEnumerable<SongDto>> GetPlaylistSongs([FromRoute]int playlistId, [FromQuery]int[] genreIds, [FromRoute] string searchTerm = "", int page = 1, int pageSize = 6, string orderByProperty = nameof(PlaylistSong.DateTimeAdded), bool orderByDescending = true)
        {
            int totalPages;
            IEnumerable<PlaylistSong> songs;
            Func<PlaylistSong, object> propertySelector;
            switch (orderByProperty)
            {
                case string propertyName when propertyName.Equals(nameof(PlaylistSong.DateTimeAdded), StringComparison.InvariantCultureIgnoreCase):
                    propertySelector = song => song.DateTimeAdded;
                    break;
                case string propertyName when propertyName.Equals(nameof(PlaylistSong.Song.ReleaseDate), StringComparison.InvariantCultureIgnoreCase):
                    propertySelector = song => song.Song!.ReleaseDate;
                    break;
                case string propertyName when propertyName.Equals(nameof(PlaylistSong.Song.Popularity), StringComparison.InvariantCultureIgnoreCase):
                    propertySelector = song => song.Song!.Popularity;
                    break;
                default:
                    return BadRequest($"There is no property called {orderByProperty} to order by.");
            }

            songs = _musicRepo.GetPlaylistSongs(playlistId, searchTerm, genreIds, propertySelector, out totalPages, orderByDescending, page, pageSize);

            LinkedList<PlaylistSongDto> songDtos = new();
            foreach (PlaylistSong song in songs)
            {
                PlaylistSongDto songDto = _mapper.Map<PlaylistSongDto>(song);
                songDtos.AddLast(songDto);
            }

            return Ok(new PageModel<PlaylistSongDto>()
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                Results = songDtos
            });
        }

        [HttpGet("genres")]
        public ActionResult<IEnumerable<string>> GetGenres() => Ok(_musicRepo.GetGenres());

        [HttpGet("{userId}/playlists")]
        public async Task<ActionResult<PageModel<Playlist>>> GetPlaylists(string userId, int page = 1, int pageSize = 6)
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return NotFound();

            IEnumerable<Playlist> playlists = _musicRepo.GetPlaylists(userId, out int totalPages, page, pageSize);
            return Ok(new PageModel<Playlist>
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                Results = playlists
            });
        }

        [HttpGet("authors")]
        public ActionResult<PageModel<AuthorDto>> GetAuthots(int page = 1, int pageSize = 6)
        {
            IEnumerable<Author> authors = _musicRepo.GetAuthors(out int totalPages, page, pageSize);
            LinkedList<AuthorDto> authorDtos = new();
            foreach (Author author in authors)
            {
                authorDtos.AddLast(_mapper.Map<AuthorDto>(author));
            }

            return Ok(new PageModel<AuthorDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                Results = authorDtos
            });
        }

        [HttpGet("playlist/belongs/to/user")]
        public ActionResult<bool> PlaylistSongIsUsers(string userId, int playlistId)
        {
            bool isPresent = _musicRepo.DoesPlaylistBelongToUser(playlistId, userId);
            return Ok(isPresent);
        }

        [HttpGet("playlist/present")]
        public async Task<ActionResult<bool>> PlaylistNamePresent([FromQuery]string userId, [FromQuery]string playlistName)
        {
            bool isPresent = await _musicRepo.IsPlaylistNamePresent(playlistName, userId);
            return Ok(isPresent);
        }

        [HttpPost("playlist/add")]
        public async Task<ActionResult<Playlist>> AddPlaylist([FromBody]Playlist playlist)
        {
            if (await _musicRepo.IsPlaylistNamePresent(playlist.Name, playlist.UserId))
                return BadRequest($"There is already a playlist called {playlist.Name}");

            playlist = _musicRepo.Add(playlist);
            return Created(string.Empty, playlist);
        }

        [HttpPost("playlist/add/song")]
        public ActionResult<Playlist> AddPlaylistSong([FromBody]PlaylistSong playlistSong)
        {
            if (playlistSong.SongId <= 0 || playlistSong.PlaylistId <= 0)
                return BadRequest("Invalid ids");

            if (_musicRepo.DoesPlaylistSongExist(playlistSong))
                return Ok();

            playlistSong = _musicRepo.Add(playlistSong);
            return Created(string.Empty, playlistSong);
        }

        [HttpPost("playlist/remove/song")]
        public ActionResult<Playlist> RemovePlaylistSong([FromBody] PlaylistSong playlistSong)
        {
            if (playlistSong.SongId <= 0 || playlistSong.PlaylistId <= 0)
                return BadRequest("Invalid ids");

            if (!_musicRepo.DoesPlaylistSongExist(playlistSong))
                return BadRequest("Playlist song with the given ids doesn't exist");

            _musicRepo.Remove(playlistSong);
            return Ok();
        }

        [HttpPost("playlist/remove")]
        public async Task<ActionResult<Playlist>> RemovePlaylist([FromBody]Playlist playlist)
        {
            if (!await _musicRepo.IsPlaylistNamePresent(playlist.Name, playlist.UserId))
                return BadRequest($"There is no playlist with the given id {playlist.Id}");

            _musicRepo.Remove(playlist);
            return Ok();
        }
    }
}
