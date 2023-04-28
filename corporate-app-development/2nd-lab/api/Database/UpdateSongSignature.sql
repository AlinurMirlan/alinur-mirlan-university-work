USE MusicPlatform
GO

CREATE OR ALTER PROC UpdateSongSignature
@songId int
AS
BEGIN
	DECLARE @albumName nvarchar(50) = N'';
	SELECT @albumName = Albums.Name FROM Songs
	INNER JOIN Albums ON Albums.Id = Songs.AlbumId
	WHERE Songs.Id = @songId;

	UPDATE Songs SET Songs.Signature = Songs.Title + @albumName
	WHERE Songs.Id = @songId;
END