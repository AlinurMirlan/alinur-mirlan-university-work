USE Flashcards
GO

CREATE OR ALTER TRIGGER OnFlashcardUpdate ON Flashcard
	AFTER UPDATE
	AS
	BEGIN
	DECLARE @repetitionIntervalBefore int,
		@repetitionIntervalAfter int,
		@flashcardId int;

	SELECT @repetitionIntervalBefore = deleted.RepetitionInterval,
		@flashcardId = deleted.Id FROM deleted;
	SET @repetitionIntervalAfter = 
		(SELECT inserted.RepetitionInterval FROM inserted);
	IF (@repetitionIntervalBefore = @repetitionIntervalAfter)
		RETURN;

	UPDATE Flashcard
		SET RepetitionDate = DATEADD(DAY, RepetitionInterval, GETDATE())
		WHERE Id = @flashcardId;
	END
GO