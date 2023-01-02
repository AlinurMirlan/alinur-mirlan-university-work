CREATE OR ALTER FUNCTION GetBookAuthorsAsString(@bookId int)
	RETURNS nvarchar(200)
	AS
	BEGIN
	RETURN (SELECT STRING_AGG(Author.Name + ' ' + Author.Surname, ', ') AS Authors
		FROM BookAuthors
		INNER JOIN Author ON BookAuthors.AuthorId = Author.Id
		INNER JOIN Book ON BookAuthors.BookId = Book.Id
		WHERE BookAuthors.BookId = @bookId
		GROUP BY Book.Title, BookAuthors.BookId);
	END
GO

CREATE OR ALTER FUNCTION GetBookGenresAsString(@bookId int)
	RETURNS nvarchar(200)
	AS
	BEGIN
	RETURN (SELECT STRING_AGG(Genre.Name, ' ') AS Genres
		FROM BookGenre
		INNER JOIN Genre ON BookGenre.GenreId = Genre.Id
		INNER JOIN Book ON BookGenre.BookId = Book.Id
		WHERE BookGenre.BookId = @bookId
		GROUP BY Book.Title, BookGenre.BookId);
	END
GO

CREATE VIEW BookInformation
	AS
	SELECT Book.Title AS BookTitle,
		dbo.GetBookAuthorsAsString(Book.Id) AS Authors,
		dbo.GetBookGenresAsString(Book.Id) AS Genres
		FROM Book;