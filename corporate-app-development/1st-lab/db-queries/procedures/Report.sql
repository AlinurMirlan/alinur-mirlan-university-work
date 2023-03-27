CREATE OR ALTER PROC IngredientsExpenditure
	@dateStart date,
	@dateEnd date
AS
BEGIN
	SELECT 
	Ingredient.Name AS IngredientName,
	SUM(DishIngredients.Amount * TabDishes.Amount) AS Amount,
	Unit.Name AS Unit,
	SUM(DishIngredients.Amount * TabDishes.Amount * Ingredient.Price) AS TotalPrice
	FROM Tab
	INNER JOIN TabDishes ON Tab.Id = TabDishes.TabId
	INNER JOIN DishIngredients ON DishIngredients.DishId = TabDishes.DishId
	INNER JOIN Ingredient ON DishIngredients.IngredientId = Ingredient.Id
	INNER JOIN Unit ON Ingredient.UnitId = Unit.Id
	WHERE Tab.OrderDate >= @dateStart
		AND Tab.OrderDate < @dateEnd
	GROUP BY Ingredient.Name, Unit.Name;
END
GO