CREATE PROC InsertIngredient 
	@name nvarchar(40),
	@price smallmoney,
	@unitId int,
	@id int OUT
AS
BEGIN
	INSERT INTO CookBook.dbo.Ingredient(Name, Price, UnitId)
		VALUES(@name, @price, @unitId);

	SET @id = SCOPE_IDENTITY();
END