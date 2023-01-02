USE Libraries
GO

CREATE OR ALTER TRIGGER LibraryBookDeletion
	ON LibraryBook
	FOR DELETE
	AS
	BEGIN
		DECLARE @libraryId int, @onStock int;
		SELECT 
			@libraryId = deleted.LibraryId,
			@onStock = deleted.OnStock 
			FROM deleted;
		UPDATE Library SET AmountOfBooks -= @onStock
			WHERE Library.Id = @libraryId;
	END