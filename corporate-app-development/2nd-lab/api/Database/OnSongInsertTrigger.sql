USE MusicPlatform
GO

CREATE OR ALTER TRIGGER OnSongInsert ON Songs
AFTER INSERT
AS
BEGIN
	DECLARE @songId int;
	SELECT @songId = inserted.Id FROM inserted;
	EXEC UpdateSongSignature @songId;
END
GO