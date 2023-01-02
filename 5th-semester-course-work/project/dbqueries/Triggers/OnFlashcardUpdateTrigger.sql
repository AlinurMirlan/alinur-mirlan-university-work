USE Flashcards
GO

CREATE OR ALTER TRIGGER OnFlashcardUpdate ON Flashcard
	AFTER UPDATE
	AS
	BEGIN
	DECLARE @interval int,
		@repetitionDateBefore date,
		@flashcardId int,
		@boxIdBefore int,
		@boxIdAfter int;

	SET @boxIdBefore = (SELECT deleted.BoxId FROM deleted);

	SELECT @interval = Box.RepetitionInterval,
		@repetitionDateBefore = inserted.RepetitionDate,
		@flashcardId = inserted.id,
		@boxIdAfter = inserted.BoxId FROM inserted
		INNER JOIN Box ON inserted.BoxId = Box.Id;

	IF (@boxIdBefore = @boxIdAfter)
		RETURN;

	DECLARE @repetitionDate date = 
		DATEADD(DAY, @interval, @repetitionDateBefore);

	UPDATE Flashcard
		SET RepetitionDate = @repetitionDate
		WHERE Id = @flashcardId;
	END
GO