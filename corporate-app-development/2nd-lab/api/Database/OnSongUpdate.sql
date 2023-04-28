USE MusicPlatform
GO

CREATE OR ALTER TRIGGER OnSongUpdate ON Songs
AFTER UPDATE
AS
BEGIN
	DECLARE @songId int, @titleBefore nvarchar(50),
	@titleAfter nvarchar(50);
	SELECT @titleBefore = deleted.Title FROM deleted;
	SELECT @songId = inserted.Id, @titleAfter = inserted.Title FROM inserted;
	IF (@titleBefore = @titleAfter)
		RETURN;

	EXEC UpdateSongSignature @songId;
END
GO