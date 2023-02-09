USE CookBook
GO

CREATE PROC GetDishIngredients
	@dishId int
AS
BEGIN
	SELECT * FROM DishIngredients
	INNER JOIN Ingredient ON DishIngredients.IngredientId = Ingredient.Id
		AND DishIngredients.DishId = @dishId;
END
GO