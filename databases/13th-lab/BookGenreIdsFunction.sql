-- Auxiliary function to get a book's genre ids for further
-- 
CREATE OR ALTER FUNCTION BookGenreIds(@bookId int)
	RETURNS TABLE
	AS
	RETURN SELECT BookGenre.GenreId AS BookGenreId FROM BookGenre
		WHERE BookGenre.BookId = @bookId;