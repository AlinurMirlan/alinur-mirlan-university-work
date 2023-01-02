USE Libraries
GO

CREATE OR ALTER PROCEDURE BookByGenre 
	@genre nvarchar(25)
	AS
	BEGIN
		DECLARE @genreId int = (SELECT Genre.Id FROM Genre 
			WHERE Genre.Name = @genre);
		IF (@genreId IS NULL)
			BEGIN
			SELECT @genre + ' genre does not exist.';
			RETURN -1;
			END

		SELECT Book.Title FROM BookGenre
			INNER JOIN Book ON BookGenre.BookId = Book.Id
			WHERE BookGenre.GenreId = @genreId;
		RETURN @@ROWCOUNT;
	END
GO

CREATE OR ALTER PROCEDURE BookByGenre;2 
	@genre1 nvarchar(25), @genre2 nvarchar(25)
	AS
	BEGIN
		DECLARE @genreId1 int = (SELECT Genre.Id FROM Genre
			WHERE Genre.Name = @genre1),
			@genreId2 int = (SELECT Genre.Id FROM Genre
			WHERE Genre.Name = @genre2);

		IF (@genreId1 IS NULL)
			BEGIN
			SELECT @genre1 + ' genre does not exist.';
			RETURN -1;
			END
		IF (@genreId2 IS NULL)
			BEGIN
			SELECT @genre2 + ' genre does not exist.';
			RETURN -1;
			END

		SELECT Book.Title FROM Book
			WHERE EXISTS (SELECT * FROM BookGenreIds(Book.Id)
				WHERE BookGenreId = @genreId1)
				AND EXISTS (SELECT * FROM BookGenreIds(Book.Id)
				WHERE BookGenreId = @genreId2);
		RETURN @@ROWCOUNT;
	END
GO

CREATE OR ALTER PROCEDURE BookByGenre;3 
	@genre1 nvarchar(25), @genre2 nvarchar(25), @genre3 nvarchar(25)
	AS
	BEGIN
		DECLARE @genreId1 int = (SELECT Genre.Id FROM Genre
			WHERE Genre.Name = @genre1),
			@genreId2 int = (SELECT Genre.Id FROM Genre
			WHERE Genre.Name = @genre2),
			@genreId3 int = (SELECT Genre.Id FROM Genre
			WHERE Genre.Name = @genre3);

		IF (@genreId1 IS NULL)
			BEGIN
			SELECT @genre1 + ' genre does not exist.';
			RETURN -1;
			END
		IF (@genreId2 IS NULL)
			BEGIN
			SELECT @genre2 + ' genre does not exist.';
			RETURN -1;
			END
		IF (@genreId3 IS NULL)
			BEGIN
			SELECT @genre3 + ' genre does not exist.';
			RETURN -1;
			END

		SELECT Book.Title FROM Book
			WHERE EXISTS (SELECT * FROM BookGenreIds(Book.Id)
				WHERE BookGenreId = @genreId1)
				AND EXISTS (SELECT * FROM BookGenreIds(Book.Id)
				WHERE BookGenreId = @genreId2)
				AND EXISTS (SELECT * FROM BookGenreIds(Book.Id)
				WHERE BookGenreId = @genreId3);
		RETURN @@ROWCOUNT;
	END
GO
