USE [University]
GO

CREATE NONCLUSTERED INDEX IX_����������_�������������_�����
	ON [dbo].[����������]([��� ����������], [�����]);

CREATE UNIQUE NONCLUSTERED INDEX IX_������_������������������
	ON [dbo].[������]([������������ ������]);

CREATE UNIQUE NONCLUSTERED INDEX IX_������_������������������_���������
	ON [dbo].[������]([������������ ������], [��� ������]);

GO

DROP INDEX IX_����������_�������������_����� ON [dbo].[�����������];