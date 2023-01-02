USE Libraries
GO

CREATE OR ALTER TRIGGER LibraryBookUpdate
	ON LibraryBook
	FOR UPDATE
	AS
	BEGIN
		DECLARE 
			@libraryId int, 
			@onStockBefore int,
			@onStockAfter int,
			@differenceInStock int;

		SET @onStockBefore = (SELECT deleted.OnStock FROM deleted);
		SELECT 
			@libraryId = inserted.LibraryId,
			@onStockAfter = inserted.OnStock 
			FROM inserted;

		SET @differenceInStock = @onStockAfter - @onStockBefore;
		UPDATE Library SET AmountOfBooks += @differenceInStock
			WHERE Library.Id = @libraryId;
	END