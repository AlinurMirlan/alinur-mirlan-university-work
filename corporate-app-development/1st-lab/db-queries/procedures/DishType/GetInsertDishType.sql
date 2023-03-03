CREATE PROC GetInsertDishType
	@dishType nvarchar(40),
	@id int OUT
AS
BEGIN
	SET @id = -1;
	SELECT TOP 1 @id = DishType.Id
	FROM DishType WHERE DishType.Name = @dishType;
	IF (@id = -1)
	BEGIN
		INSERT INTO DishType VALUES(@dishType);
		SET @id = SCOPE_IDENTITY();
	END
END