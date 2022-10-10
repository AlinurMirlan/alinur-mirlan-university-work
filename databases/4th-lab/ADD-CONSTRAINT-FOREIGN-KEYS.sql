USE University
GO

ALTER TABLE Склад
	ADD CONSTRAINT FK_Список_складов_Склад
		FOREIGN KEY (КодСкладаДвиж) REFERENCES Список_складов(КодСклада);

ALTER TABLE Склад 
	ADD CONSTRAINT FK_Типы_сырья_Склад
		FOREIGN KEY (КодСырья) REFERENCES Сырье(КодСырья);

ALTER TABLE Склад
	ADD CONSTRAINT FK_Поставщики_Склад
		FOREIGN KEY (КодПоставщика) REFERENCES Поставщики(КодПоставщика);

ALTER TABLE Склад
	ADD CONSTRAINT FK_Типы_потребителей_Склад
		FOREIGN KEY (КодПотребителя) REFERENCES Типы_потребителей(КодТипаПотреб);

ALTER TABLE Склад
	ADD CONSTRAINT FK_Покупатели_Склад
		FOREIGN KEY (КодПокупателя) REFERENCES Покупатели(КодПокупателя);

ALTER TABLE Сырье
	ADD CONSTRAINT FK_Типы_сырья_Сырье
		FOREIGN KEY (КодТипаСырья) REFERENCES Типы_сырья(КодТипаСырья);
GO