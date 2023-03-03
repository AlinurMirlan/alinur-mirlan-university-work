CREATE OR ALTER PROC DoesDishExist 
	@dishName nvarchar(40)
AS
BEGIN
	DECLARE @dishId int = -1;
	SELECT TOP 1 @dishId = Dish.Id FROM Dish
		WHERE Dish.Name = @dishName;
	IF (@dishId <> -1)
	BEGIN
		RAISERROR(N'Dish named %s already exists.', 12, 1, @dishName);
		RETURN;
	END
END
GO