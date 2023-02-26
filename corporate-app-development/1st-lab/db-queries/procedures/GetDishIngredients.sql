USE CookBook
GO

CREATE OR ALTER PROC GetDishIngredients
	@dishId int
AS
BEGIN
	SELECT Ingredient.Id, Ingredient.Name, Ingredient.Price, Unit.Name AS Unit, DishIngredients.Amount
	FROM DishIngredients
	INNER JOIN Ingredient ON DishIngredients.IngredientId = Ingredient.Id
		AND DishIngredients.DishId = @dishId
	INNER JOIN Unit ON Ingredient.UnitId = Unit.Id;
END
GO