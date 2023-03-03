USE CookBook
GO

CREATE OR ALTER PROC EditDish
	@dishId int,
	@newName nvarchar(40),
	@newType nvarchar(40)
AS
BEGIN
	DECLARE @dishTypeId int = -1;
	EXEC GetInsertDishType @newType, @dishTypeId OUTPUT;
	UPDATE Dish SET Dish.Name = @newName, Dish.DishTypeId = @dishTypeId WHERE Dish.Id = @dishId;
END