USE [University]
GO

UPDATE [dbo].[Сотрудники]
	SET Отдел = 1
	WHERE Отдел = 3 AND Зарплата <= 300;

UPDATE [dbo].[Сотрудники]
	SET Отдел = 2
	WHERE Отдел = 3 AND Зарплата > 300;

UPDATE [dbo].[Сотрудники]
	SET Зарплата = Зарплата + 100
	WHERE YEAR([Дата рождения]) = 1985;

DELETE FROM [dbo].[Сотрудники]
	WHERE Отдел = 2 AND Зарплата > 450;

TRUNCATE TABLE [dbo].[Сотрудники];

GO