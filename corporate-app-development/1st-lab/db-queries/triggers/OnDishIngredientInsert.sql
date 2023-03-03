USE CookBook
GO

CREATE OR ALTER TRIGGER OnDishIngredientInsert ON CookBook.dbo.DishIngredients
AFTER INSERT 
AS
BEGIN
	DECLARE @dishId int, @amount float, @ingredientId int;
	SELECT @dishId = inserted.DishId, 
		@amount = inserted.Amount,
		@ingredientId = inserted.IngredientId FROM inserted;
	DECLARE @ingredientPrice smallmoney = (
		SELECT TOP 1 Ingredient.Price FROM Ingredient
			WHERE Ingredient.Id = @ingredientId);
	UPDATE Dish SET Price = Price + (@amount * @ingredientPrice)
		WHERE Id = @dishId;
END