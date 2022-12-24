USE Boreas3
GO

DISABLE TRIGGER DropTriggers ON DATABASE;
DISABLE TRIGGER CreateOrAlterDbs ON DATABASE;
IF (OBJECT_ID('�������_1') IS NOT NULL)
	DROP TABLE �������_1;

SELECT * INTO �������_1 FROM �������;
GO

-- 1.1 INSTEAD OF INSERT TRIGGER
--CREATE OR ALTER TRIGGER ClientInsert ON �������_1
--	INSTEAD OF INSERT
--	AS
--	BEGIN
--	DECLARE @clientCode nvarchar(5) = (SELECT inserted.���������� FROM inserted);
--	IF (EXISTS (SELECT * FROM �������_1 WHERE �������_1.���������� = @clientCode))
--		BEGIN
--		SELECT 'Code of the client ' + @clientCode + ' is not unique. Operation was aborted.';
--		RETURN;
--		END
	
--	INSERT INTO �������_1 (
--		����������,
--		��������,
--		�����������,
--		���������,
--		�����,
--		�����,
--		�������,
--		������,
--		������,
--		�������,
--		����)
--		SELECT 
--		inserted.����������,
--		inserted.��������,
--		inserted.�����������,
--		inserted.���������,
--		inserted.�����,
--		inserted.�����,
--		inserted.�������,
--		inserted.������,
--		inserted.������,
--		inserted.�������,
--		inserted.���� FROM inserted;
--	END
--GO

-- 1.1 AFTER INSERT TRIGGER
CREATE OR ALTER TRIGGER ClientInsert ON �������_1
	FOR INSERT
	AS
	BEGIN
	DECLARE @clientCode nvarchar(5) = (SELECT inserted.���������� FROM inserted);
	IF ((SELECT COUNT(*) FROM �������_1 WHERE �������_1.���������� = @clientCode) = 2)
		BEGIN
		SELECT 'Code of the client ' + @clientCode + ' is not unique. Operation was aborted.';
		ROLLBACK TRAN;
		END
	ELSE
		SELECT * FROM inserted;
	END
GO

-- 1.2 INSTEAD OF DELETE TRIGGER
--CREATE OR ALTER TRIGGER ClientDelete ON �������_1
--	INSTEAD OF DELETE
--	AS
--	BEGIN
--	DECLARE @clientCountry nvarchar(15) = (SELECT deleted.������ FROM deleted);
--	IF (@clientCountry = N'������')
--		BEGIN
--		SELECT N'Clients from Italy are not to be deleted. Operation was aborted.';
--		RETURN;
--		END
	
--	DECLARE @clientId int = (SELECT deleted.������_Id FROM deleted); 
--	DELETE FROM �������_1 WHERE �������_1.������_Id = @clientId;
--	END
--GO

-- 1.2 INSTEAD OF DELETE TRIGGER
CREATE OR ALTER TRIGGER ClientDelete ON �������_1
	FOR DELETE
	AS
	BEGIN
	DECLARE @clientCountry nvarchar(15) = (SELECT deleted.������ FROM deleted);
	IF (@clientCountry = N'������')
		BEGIN
		SELECT N'Clients from Italy are not to be deleted. Operation was aborted.';
		ROLLBACK TRAN;
		RETURN;
		END
	
	SELECT * FROM deleted;
	END
GO

-- 1.3 INSTEAD OF UPDATE TRIGGER
--CREATE OR ALTER TRIGGER ClientUpdate ON �������_1
--	INSTEAD OF UPDATE
--	AS
--	BEGIN
--	DECLARE @clientPosition nvarchar(30) = (SELECT deleted.��������� FROM deleted);
--	IF (@clientPosition = N'������� ��������')
--		BEGIN
--		SELECT N'Chief manager clients are not to be updated. Operation was aborted.';
--		RETURN;
--		END
	
--	UPDATE �������_1 SET
--		���������� = inserted.����������,
--		�������� = inserted.��������,
--		����������� = inserted.�����������,
--		��������� = inserted.���������,
--		����� = inserted.�����,
--		����� = inserted.�����,
--		������� = inserted.�������,
--		������ = inserted.������,
--		������ = inserted.������,
--		������� = inserted.�������,
--		���� = inserted.���� FROM inserted;
--	END
--GO

CREATE OR ALTER TRIGGER ClientUpdate ON �������_1
	FOR UPDATE
	AS
	BEGIN
	DECLARE @clientPosition nvarchar(30) = (SELECT deleted.��������� FROM deleted);
	IF (@clientPosition = N'������� ��������')
		BEGIN
		SELECT N'Chief manager clients are not to be updated. Operation was aborted.';
		ROLLBACK TRAN;
		RETURN;
		END

	SELECT * FROM deleted;
	END
GO

-- 2.1 INSERT INTO
INSERT INTO �������_1 VALUES(
	'AMIOW',
	'Antonio Moreno Taqueria',
	'Frederique Citeaux',
	N'������� ��������',
	'Forsterstr. 57',
	N'���������',
	NULL,
	'WA1 1DP',
	N'������',
	'0921-12 34 65',
	'030-0076545');
-- INSERT INTO NOT UNIQUE VALUE
INSERT INTO �������_1 VALUES(
	'AMIOW',
	'Antonio Moreno Taqueria',
	'Frederique Citeaux',
	N'����������',
	'Forsterstr. 57',
	N'������',
	NULL,
	'WA1 1DP',
	N'��������������',
	'0921-12 34 65',
	'030-0076545');

SELECT * FROM �������_1 WHERE ���������� = 'AMIOW';
GO

-- DELETE FROM ITALY
DELETE FROM �������_1 WHERE ���������� = N'AMIOW';
SELECT * FROM �������_1 WHERE ���������� = N'AMIOW';
GO

-- UPDATE CHIEF MANAGER
UPDATE �������_1 SET
	����������� = 'Roland Mendel',
	��������� = '����������'
	WHERE �������_1.���������� = N'AMIOW';
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



