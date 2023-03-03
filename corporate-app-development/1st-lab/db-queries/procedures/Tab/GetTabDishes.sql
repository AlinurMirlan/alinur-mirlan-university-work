CREATE OR ALTER PROC GetTabDishes 
	@tabId int
AS
BEGIN
	SELECT Dish.Id, Dish.Name, Dish.Price, DishType.Name AS DishType, TabDishes.Amount FROM TabDishes
	INNER JOIN Dish ON TabDishes.DishId = Dish.Id AND TabDishes.TabId = @tabId
	INNER JOIN DishType ON DishType.Id = Dish.DishTypeId;
END