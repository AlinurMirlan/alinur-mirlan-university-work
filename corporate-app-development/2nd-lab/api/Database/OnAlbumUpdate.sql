USE MusicPlatform
GO

CREATE OR ALTER TRIGGER OnAlbumUpdate ON Albums
AFTER UPDATE
AS
BEGIN
	DECLARE @albumId int, @nameBefore nvarchar(50),
	@nameAfter nvarchar(50);
	SELECT @nameBefore = deleted.Name FROM deleted;
	SELECT @albumId = inserted.Id, @nameAfter = inserted.Name FROM inserted;
	IF (@nameBefore = @nameAfter)
		RETURN;
	
	DECLARE songIdsCursor CURSOR FOR 
	SELECT Songs.Id FROM Songs
	WHERE Songs.AlbumId = @albumId;

	OPEN songIdsCursor;
	DECLARE @songId int;
	FETCH NEXT FROM songIdsCursor INTO @songId
	WHILE (@@FETCH_STATUS = 0)
	BEGIN
		EXEC UpdateSongAuthorsSignature @songId;
		FETCH NEXT FROM songIdsCursor INTO @songId
	END
	CLOSE songIdsCursor;
	DEALLOCATE songIdsCursor;
END
GO