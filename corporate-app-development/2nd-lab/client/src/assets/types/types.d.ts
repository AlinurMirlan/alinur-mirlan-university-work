export type Genre = {
    id: number;
    name: string;
};
export type Author = {
    id: number;
    nickname: string;
};
export type Album = {
    id: number;
    name: string;
};
export interface Song {
    id: number;
    title: string;
    album: Album;
    releaseDate: string;
    imageFile: string;
    songFile: string;
    popularity: number;
    authors: Author[];
    genres: Genre[];
}
export interface Page {
    currentPage: number;
    pageSize: number;
    totalPages: number;
}
export interface PagedSongs extends Page {
    results: Song[];
}
export interface PagedEntities<T> extends Page {
    results: T[];
}
export interface UserCedentials {
    jwt: string;
    expiration: string;
    userId: string;
    isAdmin: boolean;
}
export interface Jwt {
    token: string;
    expiration: Date;
}
export interface PlaylistSongRaw {
    songId: number;
    playlistId: number;
    song: Song;
    dateTimeAdded: string;
}
export interface PlaylistRaw {
    id?: number;
    name: string;
    userId: string;
    dateTimeAdded?: string;
    songs?: PlaylistSongRaw[];
}
export interface PlaylistSong {
    songId: number;
    playlistId: number;
}
export interface User {
    id: string;
    name: string;
    email: string;
}