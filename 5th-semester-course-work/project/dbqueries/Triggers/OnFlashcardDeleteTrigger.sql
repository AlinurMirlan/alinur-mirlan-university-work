USE Flashcards
GO

CREATE OR ALTER TRIGGER OnFlashcardDelete ON Flashcard
	AFTER DELETE
	AS
	BEGIN
	DECLARE @deckId int = (SELECT deleted.DeckId FROM deleted)

	UPDATE Deck
		SET FlashcardsCount -= 1
		WHERE Id = @deckId;
	END
GO