CREATE OR ALTER PROC GetDish
	@dishId int
AS
BEGIN
	SELECT Dish.Id, Dish.Name, Dish.Price, DishType.Name AS DishType FROM CookBook.dbo.Dish
		INNER JOIN DishType ON Dish.DishTypeId = DishType.Id AND Dish.Id = @dishId;
END