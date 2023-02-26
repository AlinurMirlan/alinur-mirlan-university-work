CREATE OR ALTER PROC GetIngredient 
	@id int
AS
BEGIN
	SELECT Ingredient.Id, Ingredient.Name, Ingredient.Price, Unit.Name AS Unit
	FROM Ingredient INNER JOIN Unit
	ON Ingredient.UnitId = Unit.Id AND Ingredient.Id = @id;
END