ALTER TABLE University.dbo.����������
	DROP CONSTRAINT CK_����������_��������

ALTER TABLE University.dbo.����������
	ADD CONSTRAINT CK_����������_�������� CHECK(�������� >= 200 OR �������� < 500)

ALTER TABLE University.dbo.����������
	ADD CONSTRAINT UQ_�������������_������������ UNIQUE([��� ����������], [���� ��������])
	
ALTER TABLE University.dbo.����������
	ADD CONSTRAINT CK_����������_������������ CHECK(YEAR(GETDATE()) - YEAR([���� ��������]) > 15)
		
