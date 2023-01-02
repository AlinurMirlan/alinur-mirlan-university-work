CREATE OR ALTER FUNCTION GetBookAuthors()
	RETURNS TABLE
	AS
	RETURN SELECT Book.Title AS BookTitle,
		STRING_AGG(Author.Name + ' ' + Author.Surname, ', ') AS Authors
		FROM BookAuthors
		INNER JOIN Author ON BookAuthors.AuthorId = Author.Id
		INNER JOIN Book ON BookAuthors.BookId = Book.Id
		GROUP BY Book.Title, BookAuthors.BookId;