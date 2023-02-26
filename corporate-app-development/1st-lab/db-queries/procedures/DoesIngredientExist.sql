USE CookBook
GO

EXEC sys.sp_addmessage
	@msgnum = 50001,
	@severity = 12,
	@msgtext = N'Ingredient named %s does not exist.',
	@lang = 'us_english'
GO

CREATE OR ALTER PROC DoesIngredientExist 
	@ingredientName nvarchar(40)
AS
BEGIN
	DECLARE @ingredientId int = -1;
	SELECT TOP 1 @ingredientId = Ingredient.Id FROM Ingredient
		WHERE Ingredient.Name = @ingredientName;
	IF (@ingredientId = -1)
	BEGIN
		RAISERROR(50001, 12, 1, @ingredientName);
		RETURN;
	END
END
GO