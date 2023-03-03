CREATE PROC GetUnit
	@unitId int
AS
BEGIN
	SELECT TOP 1 Name FROM CookBook.dbo.Unit WHERE Id = @unitId;
END