USE [University]
GO

UPDATE [dbo].[����������]
	SET ����� = 1
	WHERE ����� = 3 AND �������� <= 300;

UPDATE [dbo].[����������]
	SET ����� = 2
	WHERE ����� = 3 AND �������� > 300;

UPDATE [dbo].[����������]
	SET �������� = �������� + 100
	WHERE YEAR([���� ��������]) = 1985;

DELETE FROM [dbo].[����������]
	WHERE ����� = 2 AND �������� > 450;

TRUNCATE TABLE [dbo].[����������];

GO