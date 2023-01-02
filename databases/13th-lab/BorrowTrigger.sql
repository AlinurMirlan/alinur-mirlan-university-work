USE Libraries
GO

CREATE OR ALTER TRIGGER OnBorrow ON Borrow
	AFTER INSERT
	AS
	BEGIN
	DECLARE @status nvarchar(15), @bookId int, @libraryId int;

	SELECT @status = inserted.Status,
		@bookId = BorrowService.BookId,
		@libraryId = BorrowService.LibraryId 
		FROM inserted
		INNER JOIN BorrowService
			ON inserted.BorrowServiceId = BorrowService.Id;

	IF (@status = 'Borrow')
		BEGIN
			UPDATE LibraryBook
				SET OnStock -= 1
				WHERE LibraryBook.BookId = @bookId
				 AND LibraryBook.LibraryId = @libraryId;
		END
	ELSE
		BEGIN
			UPDATE LibraryBook
				SET OnStock += 1
				WHERE LibraryBook.BookId = @bookId
				 AND LibraryBook.LibraryId = @libraryId;
		END
	END