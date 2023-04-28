using AutoMapper;
using MusicPlatformApi.Data.Entities;
using MusicPlatformApi.Models;

namespace MusicPlatformApi.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            base.CreateMap<Author, AuthorDto>();
            base.CreateMap<AuthorDto, Author>();
            base.CreateMap<Album, AlbumDto>();
            base.CreateMap<AlbumDto, Album>();
            base.CreateMap<Song, SongDto>()
                .AfterMap((song, songDto, context) => songDto.Authors = context.Mapper.Map<List<AuthorDto>>(song.Authors))
                .AfterMap((song, songDto, context) => songDto.Album = context.Mapper.Map<AlbumDto>(song.Album));
            base.CreateMap<SongDto, Song>()
                .AfterMap((songDto, song, context) => song.Authors = context.Mapper.Map<List<Author>>(songDto.Authors))
                .AfterMap((songDto, song, context) => song.Album = context.Mapper.Map<Album>(songDto.Album));
            base.CreateMap<PlaylistSong, PlaylistSongDto>()
                .AfterMap((song, songDto, context) => songDto.Song = context.Mapper.Map<SongDto>(song.Song));
            base.CreateMap<RegistrationCredentialModel, User>()
                .ForMember(user => user.UserName, options => options.MapFrom(userModel => userModel.Email));
            base.CreateMap<User, UserDto>();
        }
    }
}
