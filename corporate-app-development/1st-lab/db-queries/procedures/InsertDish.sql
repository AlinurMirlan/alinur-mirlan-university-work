CREATE OR ALTER PROC InsertDish 
	@name nvarchar(40),
	@price smallmoney,
	@dishTypeId int,
	@id int OUT
AS
BEGIN
	INSERT INTO CookBook.dbo.Dish(Name, Price, DishTypeId)
		VALUES(@name, @price, @dishTypeId);
		
	SET @id = SCOPE_IDENTITY();
END
GO