CREATE OR ALTER PROC InsertDishIngredient 
	@dishId int,
	@ingredientId int,
	@amount float
AS
BEGIN
	INSERT INTO CookBook.dbo.DishIngredients(DishId, IngredientId, Amount)
		VALUES(@dishId, @ingredientId, @amount);
END