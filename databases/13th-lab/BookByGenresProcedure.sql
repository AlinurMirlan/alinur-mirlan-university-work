CREATE OR ALTER PROCEDURE BookByGenres 
	@genres nvarchar(100)
	AS
	BEGIN
		DECLARE @genresCount int = (SELECT COUNT(*) FROM STRING_SPLIT(@genres, ' '));
		SELECT * FROM GetBookGenres()
			WHERE (SELECT COUNT(*) FROM STRING_SPLIT(@genres, ' ')
				WHERE Genres LIKE '%' + value + '%') = @genresCount;
	END
GO
