USE CookBook
GO

CREATE OR ALTER PROC GetIngredients
AS
BEGIN
	SELECT Ingredient.Id, Ingredient.Name, Ingredient.Price, Unit.Name AS Unit FROM Ingredient INNER JOIN Unit ON Ingredient.UnitId = Unit.Id;
END
GO