CREATE OR ALTER PROC InsertTab
	@tabNumber int,
	@tabId int OUT
AS
BEGIN
	INSERT INTO Tab(TabNumber, OrderDate) VALUES(@tabNumber, GETDATE());
	SET @tabId = SCOPE_IDENTITY();
END