USE CookBook
GO

CREATE OR ALTER PROC InsertDishIngredient 
	@dishId int,
	@ingredientName nvarchar(40),
	@amount float
AS
BEGIN
	DECLARE @ingredientId int = -1;
	SELECT TOP 1 @ingredientId = Ingredient.Id FROM Ingredient
		WHERE Ingredient.Name = @ingredientName;

	INSERT INTO CookBook.dbo.DishIngredients(DishId, IngredientId, Amount)
		VALUES(@dishId, @ingredientId, @amount);
END
GO