USE MusicPlatform
GO

CREATE OR ALTER TRIGGER OnAuthorSongInsert ON AuthorSong
AFTER INSERT
AS
BEGIN
	DECLARE @songId int, @author nvarchar(50);
	SELECT 
		@songId = inserted.SongsId,
		@author = Authors.Nickname FROM inserted
	INNER JOIN Authors ON inserted.AuthorsId = Authors.Id;

	UPDATE Songs SET Songs.Signature = Songs.Signature + @author
	WHERE Songs.Id = @songId;
END
GO