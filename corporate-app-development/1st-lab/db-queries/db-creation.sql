USE CookBook
GO

CREATE TABLE Unit 
(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(20) NOT NULL
);
GO

CREATE TABLE DishType
(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(40) NOT NULL,
);
GO

CREATE TABLE Ingredient 
(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(40) NOT NULL,
	Price smallmoney DEFAULT 0,
	UnitId int FOREIGN KEY REFERENCES Unit(Id)
);
GO

CREATE TABLE Dish
(
	Id int PRIMARY KEY IDENTITY(1, 1),
	Name nvarchar(40) NOT NULL,
	Price smallmoney DEFAULT 0,
	DishTypeId int FOREIGN KEY REFERENCES DishType(Id)
);
GO

CREATE TABLE DishIngredients
(
	DishId int FOREIGN KEY REFERENCES Dish(Id),
	IngredientId int FOREIGN KEY REFERENCES Ingredient(Id),
	Amount int NOT NULL,
	PRIMARY KEY (DishId, IngredientId)
);
GO

CREATE TABLE Tab
(
	Id int PRIMARY KEY IDENTITY(1, 1),
	TabNumber int NOT NULL,
	OrderDate smalldatetime NOT NULL
);
GO

CREATE TABLE TabDishes 
(
	TabId int FOREIGN KEY REFERENCES Tab(Id),
	DishId int FOREIGN KEY REFERENCES Dish(Id),
	Amount int NOT NULL,
	PRIMARY KEY (TabId, DishId)
);
GO