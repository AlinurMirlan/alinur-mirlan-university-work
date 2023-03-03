USE CookBook
GO

CREATE OR ALTER TRIGGER OnDishIngredientDelete ON CookBook.dbo.DishIngredients
AFTER DELETE 
AS
BEGIN
	DECLARE @dishId int, @amount float, @ingredientId int;
	SELECT @dishId = deleted.DishId, 
		@amount = deleted.Amount,
		@ingredientId = deleted.IngredientId FROM deleted;
	DECLARE @ingredientPrice smallmoney = (
		SELECT TOP 1 Ingredient.Price FROM Ingredient
			WHERE Ingredient.Id = @ingredientId);
	UPDATE Dish SET Price = Price - (@amount * @ingredientPrice)
		WHERE Id = @dishId;
END