USE CookBook
GO

CREATE OR ALTER PROC DeleteDish
	@dishId int
AS
BEGIN
	EXEC DeleteDishIngredients @dishId;
	DELETE FROM Dish WHERE Dish.Id = @dishId;
END
GO