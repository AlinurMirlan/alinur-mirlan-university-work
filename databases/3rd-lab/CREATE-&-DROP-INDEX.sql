USE [University]
GO

CREATE NONCLUSTERED INDEX IX_Сотрудники_ФиоСотрудника_Отдел
	ON [dbo].[Сотрудники]([ФИО Сотрудника], [Отдел]);

CREATE UNIQUE NONCLUSTERED INDEX IX_Страны_НаименованиеСтраны
	ON [dbo].[Страны]([Наименование страны]);

CREATE UNIQUE NONCLUSTERED INDEX IX_Города_НаименованиеГорода_КодСтраны
	ON [dbo].[Города]([Наименование города], [Код страны]);

GO

DROP INDEX IX_Сотрудники_ФиоСотрудника_Отдел ON [dbo].[Сотдрудники];