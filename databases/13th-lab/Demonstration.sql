USE Libraries
GO

EXEC BookByAuthors 'diana, jake';
EXEC BookByGenres 'novel romance';

SELECT * FROM BookInformation;
SELECT * FROM GetBookGenres();

-- Select books and all of their genres in one string
SELECT Book.Title AS BookTitle,
	STRING_AGG(Genre.Name, ' ') AS Genres
	FROM BookGenre
	INNER JOIN Genre ON BookGenre.GenreId = Genre.Id
	INNER JOIN Book ON BookGenre.BookId = Book.Id
	GROUP BY Book.Title, BookGenre.BookId;
GO

SELECT * FROM GetBookAuthors();

-- Finding books by a given genre.
SELECT 'Romance books';
EXEC BookByGenre 'Romance';

-- Finding books by the combination of genres.
SELECT 'Fiction Romance books';
EXEC BookByGenre;2 'Fiction', 'Romance';

SELECT 'Fiction Novel Romance books';
EXEC BookByGenre;3 'Fiction', 'Novel', 'Romance';

-- Invalid data handling.
EXEC BookByGenre 'Scientific';

EXEC BookByAuthor 1;
EXEC BookByAuthor;2 4, 5;