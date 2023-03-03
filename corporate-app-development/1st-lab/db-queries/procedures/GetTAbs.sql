CREATE OR ALTER PROC GetTabs 
	@orderByDescending bit
AS
BEGIN
	IF (@orderByDescending = 1)
		SELECT Tab.TabNumber, Tab.OrderDate FROM Tab ORDER BY Tab.OrderDate DESC;
	ELSE
		SELECT Tab.TabNumber, Tab.OrderDate FROM Tab ORDER BY Tab.OrderDate ASC;
END