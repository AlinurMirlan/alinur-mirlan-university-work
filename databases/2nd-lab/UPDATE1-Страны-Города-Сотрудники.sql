ALTER TABLE University.dbo.����������
	ADD [�������� ���������] nvarchar(3)
		CHECK(LOWER([�������� ���������]) = N'��' OR LOWER([�������� ���������]) = N'���')
		
