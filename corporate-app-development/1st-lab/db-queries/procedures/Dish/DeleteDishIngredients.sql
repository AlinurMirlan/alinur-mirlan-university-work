USE CookBook
GO

CREATE OR ALTER PROC DeleteDishIngredients
	@dishId int,
	@ingredientId int = -1
AS
BEGIN
	IF (@ingredientId <> -1)
	BEGIN
		DELETE FROM DishIngredients WHERE DishIngredients.DishId = @dishId 
			AND DishIngredients.IngredientId = @ingredientId;
		RETURN
	END	
	DELETE FROM DishIngredients WHERE DishIngredients.DishId = @dishId;
END