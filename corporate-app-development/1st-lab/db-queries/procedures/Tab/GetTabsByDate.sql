USE CookBook
GO

CREATE OR ALTER PROC GetTabsByDate
	@date date,
	@orderByDescending bit
AS
BEGIN
	IF (@orderByDescending = 1)
		SELECT * FROM Tab WHERE CONVERT(date, Tab.OrderDate) = @date ORDER BY Tab.OrderDate DESC;
	ELSE
		SELECT * FROM Tab WHERE CONVERT(date, Tab.OrderDate) = @date ORDER BY Tab.OrderDate ASC;
END