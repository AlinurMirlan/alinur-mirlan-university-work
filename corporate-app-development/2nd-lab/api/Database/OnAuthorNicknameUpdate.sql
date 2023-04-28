USE MusicPlatform
GO

CREATE OR ALTER TRIGGER OnAuthorNicknameUpdate ON Authors
FOR UPDATE
AS
BEGIN
	DECLARE @nameBefore nvarchar(50), @nameAfter nvarchar(50), @authorId int
	SELECT @nameBefore = deleted.Nickname FROM deleted;
	SELECT @nameAfter = inserted.Nickname, @authorId = inserted.Id FROM inserted;
	IF (@nameBefore = @nameAfter)
		RETURN;

	DECLARE songIdsCursor CURSOR FOR 
	SELECT AuthorSong.SongsId FROM AuthorSong
	WHERE AuthorSong.AuthorsId = @authorId;

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