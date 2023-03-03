CREATE OR ALTER PROC EditIngredient
	@ingredientId int,
	@newName nvarchar(40),
	@newPrice smallmoney,
	@newUnit nvarchar(20)
AS
BEGIN
	DECLARE @unitId int = -1;
	SELECT TOP 1 @unitId = Unit.Id FROM Unit WHERE Unit.Name = @newUnit;
	UPDATE Ingredient SET Name = @newName, Price = @newPrice, UnitId = @unitId WHERE Id = @ingredientId;
END