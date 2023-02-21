CREATE PROC GetDish
	@dishId int
AS
BEGIN
	SELECT * FROM CookBook.dbo.Dish WHERE Id = @dishId;
END