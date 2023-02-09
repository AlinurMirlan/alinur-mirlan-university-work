CREATE OR ALTER PROC DeleteIngredient
	@ingredientId int
AS
BEGIN
	DELETE FROM CookBook.dbo.Ingredient WHERE Id = @ingredientId;
END
GO