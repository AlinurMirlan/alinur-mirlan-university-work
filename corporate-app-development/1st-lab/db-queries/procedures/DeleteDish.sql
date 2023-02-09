CREATE OR ALTER PROC DeleteDish
	@dishId int
AS
BEGIN
	DELETE FROM CookBook.dbo.Dish WHERE Id = @dishId;
END
GO