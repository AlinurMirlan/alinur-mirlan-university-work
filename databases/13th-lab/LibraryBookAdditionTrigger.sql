USE Libraries
GO

CREATE OR ALTER TRIGGER LibraryBookAddition
	ON LibraryBook
	FOR INSERT
	AS
	BEGIN
		DECLARE @libraryId int, @onStock int;
		SELECT 
			@libraryId = inserted.LibraryId,
			@onStock = inserted.OnStock 
			FROM inserted;
		UPDATE Library SET AmountOfBooks += @onStock
			WHERE Library.Id = @libraryId;
	END