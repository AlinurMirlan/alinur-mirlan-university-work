USE Libraries
GO

CREATE OR ALTER TRIGGER OnBorrowingService ON BorrowService
	FOR INSERT
	AS
	BEGIN
	DECLARE @bookId int, @libraryId int, @onStock int;

	SELECT @bookId = inserted.BookId,
		@libraryId = inserted.LibraryId 
		FROM inserted;

	SET @onStock = (SELECT LibraryBook.OnStock FROM LibraryBook
		WHERE LibraryBook.LibraryId = @libraryId
			AND LibraryBook.BookId = @bookId);

	IF (@onStock = 0)
		ROLLBACK TRAN;
	END