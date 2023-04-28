import type { UserCedentials, PlaylistRaw, PlaylistSongRaw, Song } from './types/types';

export class Jwt {
    token: string;
    expiration: Date;
    constructor(jwtRaw: UserCedentials) {
        this.token = jwtRaw.jwt;
        this.expiration = new Date(Date.parse(jwtRaw.expiration));
    }
}   
export class PlaylistSong {
    songId: number;
    playlistId: number;
    song: Song;
    dateTimeAdded: Date;
    constructor(playlistSongRaw: PlaylistSongRaw) {
        this.songId = playlistSongRaw.songId;
        this.playlistId = playlistSongRaw.playlistId;
        this.song = playlistSongRaw.song;
        this.dateTimeAdded = new Date(Date.parse(playlistSongRaw.dateTimeAdded));
    }
}
export class Playlist {
    id?: number;
    name: string;
    userId: string;
    dateTimeAdded?: Date;
    songs: PlaylistSong[];
    constructor(playlistRaw: PlaylistRaw) {
        this.id =  playlistRaw.id;
        this.name = playlistRaw.name;
        this.userId = playlistRaw.userId;
        this.dateTimeAdded = new Date(Date.parse(playlistRaw.dateTimeAdded || Date.now.toString()));
        this.songs = [];
        playlistRaw.songs?.forEach(song => {
            this.songs.push(new PlaylistSong(song));
        })
    }
}
