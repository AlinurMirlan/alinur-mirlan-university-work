USE MusicPlatform
GO

CREATE OR ALTER PROC UpdateSongAuthorsSignature
@songId int
AS
BEGIN
	EXEC UpdateSongSignature @songId;

	UPDATE Songs SET
	Songs.Signature = Songs.Signature + Authors.Nickname
	FROM Songs
	INNER JOIN AuthorSong ON AuthorSong.SongsId = Songs.Id
	INNER JOIN Authors ON Authors.Id = AuthorSong.AuthorsId
	WHERE AuthorSong.SongsId = Songs.Id;
END