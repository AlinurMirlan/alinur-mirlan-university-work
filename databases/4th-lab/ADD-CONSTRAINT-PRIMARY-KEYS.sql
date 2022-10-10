USE University
GO

ALTER TABLE Типы_сырья
	ADD CONSTRAINT PK_Типы_сырья PRIMARY KEY (КодТипаСырья);

ALTER TABLE Сырье
	ADD CONSTRAINT PK_Сырье PRIMARY KEY (КодСырья);

ALTER TABLE Склад
	ADD CONSTRAINT PK_Склад PRIMARY KEY (КодДвижения);

ALTER TABLE Поставщики
	ADD CONSTRAINT PK_Поставщики PRIMARY KEY (КодПоставщика);

ALTER TABLE Покупатели
	ADD CONSTRAINT PK_Покупатели PRIMARY KEY (КодПокупателя);

ALTER TABLE Ед_изм
	ADD CONSTRAINT PK_Ед_изм PRIMARY KEY (КодЕдИзм);

ALTER TABLE Типы_потребителей
	ADD CONSTRAINT PK_Типы_потребителей PRIMARY KEY (КодТипаПотреб);
	
ALTER TABLE Список_складов
	ADD CONSTRAINT PK_Список_складов PRIMARY KEY (КодСклада);
GO



