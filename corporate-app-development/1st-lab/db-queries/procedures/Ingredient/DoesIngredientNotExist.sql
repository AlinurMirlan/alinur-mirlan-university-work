CREATE OR ALTER PROC DoesIngredientNotExist 
	@ingredientName nvarchar(40)
AS
BEGIN
	DECLARE @ingredientId int = -1;
	SELECT TOP 1 @ingredientId = Ingredient.Id FROM Ingredient
		WHERE Ingredient.Name = @ingredientName;
	IF (@ingredientId <> -1)
	BEGIN
		RAISERROR(N'Ingredient named %s already exists.', 12, 1, @ingredientName);
		RETURN;
	END
END
GO