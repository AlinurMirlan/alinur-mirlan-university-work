USE Boreas3
GO

DISABLE TRIGGER DropTriggers ON DATABASE;
DISABLE TRIGGER CreateOrAlterDbs ON DATABASE;
IF (OBJECT_ID('Клиенты_1') IS NOT NULL)
	DROP TABLE Клиенты_1;

SELECT * INTO Клиенты_1 FROM Клиенты;
GO

-- 1.1 INSTEAD OF INSERT TRIGGER
--CREATE OR ALTER TRIGGER ClientInsert ON Клиенты_1
--	INSTEAD OF INSERT
--	AS
--	BEGIN
--	DECLARE @clientCode nvarchar(5) = (SELECT inserted.КодКлиента FROM inserted);
--	IF (EXISTS (SELECT * FROM Клиенты_1 WHERE Клиенты_1.КодКлиента = @clientCode))
--		BEGIN
--		SELECT 'Code of the client ' + @clientCode + ' is not unique. Operation was aborted.';
--		RETURN;
--		END
	
--	INSERT INTO Клиенты_1 (
--		КодКлиента,
--		Название,
--		ОбращатьсяК,
--		Должность,
--		Адрес,
--		Город,
--		Область,
--		Индекс,
--		Страна,
--		Телефон,
--		Факс)
--		SELECT 
--		inserted.КодКлиента,
--		inserted.Название,
--		inserted.ОбращатьсяК,
--		inserted.Должность,
--		inserted.Адрес,
--		inserted.Город,
--		inserted.Область,
--		inserted.Индекс,
--		inserted.Страна,
--		inserted.Телефон,
--		inserted.Факс FROM inserted;
--	END
--GO

-- 1.1 AFTER INSERT TRIGGER
CREATE OR ALTER TRIGGER ClientInsert ON Клиенты_1
	FOR INSERT
	AS
	BEGIN
	DECLARE @clientCode nvarchar(5) = (SELECT inserted.КодКлиента FROM inserted);
	IF ((SELECT COUNT(*) FROM Клиенты_1 WHERE Клиенты_1.КодКлиента = @clientCode) = 2)
		BEGIN
		SELECT 'Code of the client ' + @clientCode + ' is not unique. Operation was aborted.';
		ROLLBACK TRAN;
		END
	ELSE
		SELECT * FROM inserted;
	END
GO

-- 1.2 INSTEAD OF DELETE TRIGGER
--CREATE OR ALTER TRIGGER ClientDelete ON Клиенты_1
--	INSTEAD OF DELETE
--	AS
--	BEGIN
--	DECLARE @clientCountry nvarchar(15) = (SELECT deleted.Страна FROM deleted);
--	IF (@clientCountry = N'Италия')
--		BEGIN
--		SELECT N'Clients from Italy are not to be deleted. Operation was aborted.';
--		RETURN;
--		END
	
--	DECLARE @clientId int = (SELECT deleted.Клиент_Id FROM deleted); 
--	DELETE FROM Клиенты_1 WHERE Клиенты_1.Клиент_Id = @clientId;
--	END
--GO

-- 1.2 INSTEAD OF DELETE TRIGGER
CREATE OR ALTER TRIGGER ClientDelete ON Клиенты_1
	FOR DELETE
	AS
	BEGIN
	DECLARE @clientCountry nvarchar(15) = (SELECT deleted.Страна FROM deleted);
	IF (@clientCountry = N'Италия')
		BEGIN
		SELECT N'Clients from Italy are not to be deleted. Operation was aborted.';
		ROLLBACK TRAN;
		RETURN;
		END
	
	SELECT * FROM deleted;
	END
GO

-- 1.3 INSTEAD OF UPDATE TRIGGER
--CREATE OR ALTER TRIGGER ClientUpdate ON Клиенты_1
--	INSTEAD OF UPDATE
--	AS
--	BEGIN
--	DECLARE @clientPosition nvarchar(30) = (SELECT deleted.Должность FROM deleted);
--	IF (@clientPosition = N'Главный менеджер')
--		BEGIN
--		SELECT N'Chief manager clients are not to be updated. Operation was aborted.';
--		RETURN;
--		END
	
--	UPDATE Клиенты_1 SET
--		КодКлиента = inserted.КодКлиента,
--		Название = inserted.Название,
--		ОбращатьсяК = inserted.ОбращатьсяК,
--		Должность = inserted.Должность,
--		Адрес = inserted.Адрес,
--		Город = inserted.Город,
--		Область = inserted.Область,
--		Индекс = inserted.Индекс,
--		Страна = inserted.Страна,
--		Телефон = inserted.Телефон,
--		Факс = inserted.Факс FROM inserted;
--	END
--GO

CREATE OR ALTER TRIGGER ClientUpdate ON Клиенты_1
	FOR UPDATE
	AS
	BEGIN
	DECLARE @clientPosition nvarchar(30) = (SELECT deleted.Должность FROM deleted);
	IF (@clientPosition = N'Главный менеджер')
		BEGIN
		SELECT N'Chief manager clients are not to be updated. Operation was aborted.';
		ROLLBACK TRAN;
		RETURN;
		END

	SELECT * FROM deleted;
	END
GO

-- 2.1 INSERT INTO
INSERT INTO Клиенты_1 VALUES(
	'AMIOW',
	'Antonio Moreno Taqueria',
	'Frederique Citeaux',
	N'Главный менеджер',
	'Forsterstr. 57',
	N'Манчестер',
	NULL,
	'WA1 1DP',
	N'Италия',
	'0921-12 34 65',
	'030-0076545');
-- INSERT INTO NOT UNIQUE VALUE
INSERT INTO Клиенты_1 VALUES(
	'AMIOW',
	'Antonio Moreno Taqueria',
	'Frederique Citeaux',
	N'Совладелец',
	'Forsterstr. 57',
	N'Лондон',
	NULL,
	'WA1 1DP',
	N'Великобритания',
	'0921-12 34 65',
	'030-0076545');

SELECT * FROM Клиенты_1 WHERE КодКлиента = 'AMIOW';
GO

-- DELETE FROM ITALY
DELETE FROM Клиенты_1 WHERE КодКлиента = N'AMIOW';
SELECT * FROM Клиенты_1 WHERE КодКлиента = N'AMIOW';
GO

-- UPDATE CHIEF MANAGER
UPDATE Клиенты_1 SET
	ОбращатьсяК = 'Roland Mendel',
	Должность = 'Совладелец'
	WHERE Клиенты_1.КодКлиента = N'AMIOW';
GO

-- 2.1 DATABASE TRIGGER ON TABLE CREATION AND DELETION
CREATE OR ALTER TRIGGER CreateOrAlterDbs ON DATABASE
	FOR CREATE_TABLE, ALTER_TABLE
	AS
	BEGIN
	PRINT 'Table creation or alteration is not allowed.';
	ROLLBACK TRAN;
	END
GO

-- 2.2 DATABASE TRIGGER ON DROP TRIGGER
CREATE OR ALTER TRIGGER DropTriggers ON DATABASE
	FOR DROP_TRIGGER
	AS
	BEGIN
	PRINT 'Trigger deletion is not allowed.';
	ROLLBACK TRAN;
	END
GO

CREATE TABLE SomeTable (
	SomeId int IDENTITY(1, 1)
);
GO

DROP TRIGGER ClientUpdate;
GO



