CREATE OR ALTER FUNCTION GetBookGenres()
	RETURNS TABLE
	AS
	RETURN SELECT Book.Title AS BookTitle,
		STRING_AGG(Genre.Name, ' ') AS Genres
		FROM BookGenre
		INNER JOIN Genre ON BookGenre.GenreId = Genre.Id
		INNER JOIN Book ON BookGenre.BookId = Book.Id
		GROUP BY Book.Title, BookGenre.BookId;