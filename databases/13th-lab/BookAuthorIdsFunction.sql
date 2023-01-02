-- Auxiliary function to get a book's author ids for further
-- 
CREATE OR ALTER FUNCTION BookAuthorIds(@bookId int)
	RETURNS TABLE
	AS
	RETURN SELECT BookAuthors.AuthorId AS BookAuthorId FROM BookAuthors
		WHERE BookAuthors.BookId = @bookId;