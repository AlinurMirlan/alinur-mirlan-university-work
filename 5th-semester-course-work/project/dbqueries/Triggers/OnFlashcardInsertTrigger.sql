USE Flashcards
GO

CREATE OR ALTER TRIGGER OnFlashcardInsert ON Flashcard
	AFTER INSERT
	AS
	BEGIN
	DECLARE @deckId int = (SELECT inserted.DeckId FROM inserted)

	UPDATE Deck
		SET FlashcardsCount += 1
		WHERE Id = @deckId;
	END
GO