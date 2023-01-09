USE Flashcards
GO

CREATE OR ALTER TRIGGER OnFlashcardInsert ON Flashcard
	AFTER INSERT
	AS
	BEGIN
	DECLARE @deckId int,
		@flashcardId int,
		@repetitionDate date,
		@interval int;

	SELECT @deckId = inserted.DeckId,
		@flashcardId = inserted.Id,
		@repetitionDate = inserted.RepetitionDate,
		@interval = inserted.RepetitionInterval
		FROM inserted;

	IF (@repetitionDate IS NULL)
		BEGIN
		SET @repetitionDate = DATEADD(DAY, @interval, GETDATE());
			UPDATE Flashcard
				SET RepetitionDate = @repetitionDate
				WHERE Id = @flashcardId;
		END

	UPDATE Deck
		SET FlashcardsCount += 1
		WHERE Id = @deckId;
	END
GO