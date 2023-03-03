CREATE OR ALTER PROC InsertTabDish
	@tabId int,
	@dishId int,
	@dishCount int
AS
BEGIN
	INSERT INTO TabDishes VALUES(@tabId, @dishId, @dishCount);
END