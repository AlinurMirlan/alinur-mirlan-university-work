USE Libraries
GO

CREATE OR ALTER PROCEDURE BookByAuthor 
	@authorId int
	AS
	BEGIN
		SELECT Book.Title FROM BookAuthors
			INNER JOIN Book ON BookAuthors.BookId = Book.Id
			WHERE BookAuthors.AuthorId = @authorId;
		RETURN @@ROWCOUNT;
	END
GO

CREATE OR ALTER PROCEDURE BookByAuthor;2 
	@authorId1 int, @authorId2 int
	AS
	BEGIN
		SELECT Book.Title FROM Book
			WHERE EXISTS (SELECT * FROM BookAuthorIds(Book.Id)
				WHERE BookAuthorId = @authorId1)
				AND EXISTS (SELECT * FROM BookAuthorIds(Book.Id)
				WHERE BookAuthorId = @authorId2);
		RETURN @@ROWCOUNT;
	END
GO

CREATE OR ALTER PROCEDURE BookByAuthor;3 
	@authorId1 int, @authorId2 int, @authorId3 int
	AS
	BEGIN
		SELECT Book.Title FROM Book
			WHERE EXISTS (SELECT * FROM BookAuthorIds(Book.Id)
				WHERE BookAuthorId = @authorId1)
				AND EXISTS (SELECT * FROM BookAuthorIds(Book.Id)
				WHERE BookAuthorId = @authorId2)
				AND EXISTS (SELECT * FROM BookAuthorIds(Book.Id)
				WHERE BookAuthorId = @authorId3);
		RETURN @@ROWCOUNT;
	END
GO
