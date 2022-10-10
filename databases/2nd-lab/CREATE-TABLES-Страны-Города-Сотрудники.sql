CREATE TABLE University.dbo.������
(
	[��� ������] int PRIMARY KEY IDENTITY(1, 1),
	[������������ ������] nvarchar(30) NOT NULL
);

CREATE TABLE University.dbo.������ 
(
	[��� ������] int PRIMARY KEY IDENTITY(1, 1),
	[������������ ������] nvarchar(30) NOT NULL,
	[��� ������] int NOT NULL FOREIGN KEY REFERENCES University.dbo.������([��� ������])
	ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT UQ_������_������������������_��������� UNIQUE([������������ ������], [��� ������])
);

CREATE TABLE University.dbo.���������� 
(
	[��� ����������] int PRIMARY KEY IDENTITY(1, 1),
	[��� ����������] nvarchar(100) NOT NULL,
	[���� ��������] date NOT NULL,
	����� nvarchar(100) NOT NULL,
	�������� real NOT NULL CHECK(�������� >= 200),
	[��� ������] int NOT NULL,
	CONSTRAINT FK_������_���������� FOREIGN KEY ([��� ������]) REFERENCES University.dbo.������ ([��� ������])
	ON DELETE CASCADE ON UPDATE CASCADE
)