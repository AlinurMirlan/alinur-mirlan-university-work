USE Libraries
GO

CREATE OR  ALTER VIEW BookInfo
	AS SELECT
		Book.Title,
		COUNT(BookGenre.GenreId) AS Genres,
		Person.Name AS Author,
		Country.Name AS Country
		FROM BookGenre
		INNER JOIN Book ON BookGenre.BookId = Book.Id
		INNER JOIN Person ON Book.AuthorId = Person.Id
		INNER JOIN Country ON Person.CountryId = Country.Id
		GROUP BY Book.Title, Person.Name, Country.Name, BookGenre.BookId;


