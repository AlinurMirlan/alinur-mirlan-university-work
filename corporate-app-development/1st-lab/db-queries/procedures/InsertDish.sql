CREATE OR ALTER PROC InsertDish 
	@name nvarchar(40),
	@price smallmoney,
	@dishType nvarchar(40),
	@id int OUT
AS
BEGIN
	DECLARE @dishTypeId int = -1;
	EXEC GetInsertDishType @dishType, @id = @dishTypeId OUTPUT;

	BEGIN TRY
		INSERT INTO CookBook.dbo.Dish(Name, Price, DishTypeId)
			VALUES(@name, @price, @dishTypeId);
	END TRY
	BEGIN CATCH
		IF (ERROR_NUMBER() <> 2601)
			THROW;

		RAISERROR(N'Dish named %s already exists.', 12, 1, @name);
	END CATCH
		
	SET @id = SCOPE_IDENTITY();
	SELECT @id;
END
GO