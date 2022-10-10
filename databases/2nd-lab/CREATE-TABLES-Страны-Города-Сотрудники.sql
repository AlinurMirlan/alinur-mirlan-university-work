CREATE TABLE University.dbo.Страны
(
	[Код страны] int PRIMARY KEY IDENTITY(1, 1),
	[Наименование страны] nvarchar(30) NOT NULL
);

CREATE TABLE University.dbo.Города 
(
	[Код города] int PRIMARY KEY IDENTITY(1, 1),
	[Наименование города] nvarchar(30) NOT NULL,
	[Код страны] int NOT NULL FOREIGN KEY REFERENCES University.dbo.Страны([Код страны])
	ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT UQ_Города_НаименованиеГорода_КодСтраны UNIQUE([Наименование Города], [Код страны])
);

CREATE TABLE University.dbo.Сотрудники 
(
	[Код сотрудника] int PRIMARY KEY IDENTITY(1, 1),
	[ФИО сотрудника] nvarchar(100) NOT NULL,
	[Дата рождения] date NOT NULL,
	Отдел nvarchar(100) NOT NULL,
	Зарплата real NOT NULL CHECK(Зарплата >= 200),
	[Код города] int NOT NULL,
	CONSTRAINT FK_Города_Сотрудники FOREIGN KEY ([Код города]) REFERENCES University.dbo.Города ([Код города])
	ON DELETE CASCADE ON UPDATE CASCADE
)