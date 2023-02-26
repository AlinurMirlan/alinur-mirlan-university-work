CREATE OR ALTER PROC InsertIngredient 
	@name nvarchar(40),
	@price smallmoney,
	@unitName nvarchar(20),
	@id int OUT
AS
BEGIN
	DECLARE @unitId int = -1;
	SELECT TOP 1 @unitId = Id FROM CookBook.dbo.Unit WHERE Name = @unitName;
	if (@unitId = -1)
		THROW 50000, 'Unit name does not exist.', 1;

	INSERT INTO CookBook.dbo.Ingredient(Name, Price, UnitId)
		VALUES(@name, @price, @unitId);

	SET @id = SCOPE_IDENTITY();
END