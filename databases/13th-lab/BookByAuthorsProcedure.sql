CREATE OR ALTER PROCEDURE BookByAuthors 
	@authors nvarchar(100)
	AS
	BEGIN
		DECLARE @authorsCount int = (SELECT COUNT(*) FROM STRING_SPLIT(@authors, ','));
		SELECT * FROM GetBookAuthors()
			WHERE (SELECT COUNT(*) FROM STRING_SPLIT(@authors, ',')
				WHERE Authors LIKE '%' + TRIM(value) + '%') = @authorsCount;
	END
GO
