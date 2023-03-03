USE CookBook
GO

CREATE PROC EditDishIngredientAmount
	@dishId int,
	@ingredientId int,
	@newAmount int
AS
BEGIN
	UPDATE DishIngredients SET Amount = @newAmount
		WHERE DishId = @dishId AND IngredientId = @ingredientId;
END