USE Flashcards
GO

CREATE OR ALTER TRIGGER OnFlashcardInsert ON Flashcard
	INSTEAD OF INSERT
	AS
	BEGIN
	DECLARE @deckId int,
		@front nvarchar(max),
		@back nvarchar(max),
		@creationDate date,
		@repetitionDate date,
		@interval int;

	SELECT @deckId = inserted.DeckId,
		@front = inserted.Front,
		@back = inserted.Back,
		@creationDate = inserted.CreationDate,
		@repetitionDate = inserted.RepetitionDate,
		@interval = inserted.RepetitionInterval
		FROM inserted;

	IF (@repetitionDate IS NULL)
		SET @repetitionDate = DATEADD(DAY, @interval, GETDATE());

	INSERT INTO Flashcard VALUES
	(
		@deckId,
		@front,
		@back,
		@creationDate,
		@repetitionDate,
		@interval
	);
	SELECT Flashcard.Id FROM Flashcard WHERE @@ROWCOUNT > 0 AND Flashcard.Id = scope_identity();

	UPDATE Deck
		SET FlashcardsCount += 1
		WHERE Id = @deckId;
	END
GO