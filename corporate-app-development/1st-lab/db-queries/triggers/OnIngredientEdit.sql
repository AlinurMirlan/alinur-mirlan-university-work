USE CookBook
GO

CREATE OR ALTER TRIGGER OnIngredientPriceEdit ON CookBook.dbo.Ingredient
AFTER UPDATE 
AS
BEGIN
	DECLARE @oldPrice smallmoney = (
		SELECT Price FROM deleted);

	DECLARE @ingredientId int, @newPrice smallmoney;
	SELECT @ingredientId = inserted.Id, 
		@newPrice = inserted.Price FROM inserted;

	UPDATE Dish SET Price = Price - (DishIngredients.Amount * @oldPrice)
		+ (DishIngredients.Amount * @newPrice) 
		FROM DishIngredients WHERE DishIngredients.IngredientId = @ingredientId
		AND DishIngredients.DishId = Dish.Id;
END