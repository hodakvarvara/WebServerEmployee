CREATE DATABASE EmployeeDb
GO

USE EmployeeDb
GO


CREATE TABLE Passport( 
  PassportID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Passport PRIMARY KEY, 
  [Type] nvarchar (50) NOT NULL, 
  Number nvarchar (50) NOT NULL
)
CREATE TABLE Company( 
  CompanyID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Company PRIMARY KEY, 
  [Name] nvarchar (100) NOT NULL 
)
CREATE TABLE Department( 
  DepartmentID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Department PRIMARY KEY, 
  [Name] nvarchar (100) NOT NULL, 
  Phone_number nvarchar (11) NOT NULL,
  CompanyID int NOT NULL
  CONSTRAINT FK_Department_CompanyID
  FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID) ON DELETE CASCADE
)
CREATE TABLE Employee( 
  EmployeeID int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Employee PRIMARY KEY, 
  [Name] nvarchar (50) NOT NULL, 
  Surname nvarchar (50) NOT NULL, 
  Phone_number nvarchar (20) NOT NULL UNIQUE, 
  CompanyID int NOT NULL,
  PassportID int NOT NULL, 
  DepartmentID int NOT NULL,
  CONSTRAINT FK_Employee_CompanyID
  FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID),
  CONSTRAINT FK_Employee_PassportID
  FOREIGN KEY (PassportID) REFERENCES Passport(PassportID) ON DELETE CASCADE,
  CONSTRAINT FK_Employee_DepartmentID
  FOREIGN KEY (DepartmentID) REFERENCES Department(DepartmentID) ON DELETE CASCADE
)

INSERT Passport([Type], Number) VALUES
('P', N'1233 123456'),
('D', N'2134 123456'),
('S', N'5678 123456'),
('P', N'7855 123456'),
('P', N'7853 123453'),
('P', N'2353 123480'),
('P', N'2122 134210'),
('P', N'2124 134210'),
('P', N'2125 134214'),
('P', N'2126 134216'),
('P', N'2226 144216'),
('P', N'2626 124216'),
('P', N'2526 134211'),
('D', N'5785 123456')
SELECT*FROM Passport

INSERT Company([Name]) VALUES
('Smartway'),
('Company'),
(N'Сбербанк')
SELECT*FROM Company

INSERT Department([Name], Phone_number, CompanyID) VALUES
('IT', N'3416', 1),
('Front-end', N'1515', 1),
('Back-end', N'1516', 1),
(N'Отдел кадров', N'3419', 2),
(N'Отдел автоматизации', N'1123', 2),
(N'Маркетинговый отдел', N'2133', 3),
(N'Бухгалтерия', N'4321', 3)
SELECT*FROM Department

INSERT Employee([Name], Surname, Phone_number, CompanyID, PassportID, DepartmentID) VALUES
(N'Иван1', N'Иванов1', N'89873897021', 1, 1, 1),
(N'Петр', N'Петров', N'89873897025', 1, 5, 1),
(N'Иван2', N'Иванов2', N'89873897034', 1, 2, 2),
(N'Кирилл', N'Ульянов', N'89873897023', 1, 7, 2),
(N'Варвара', N'Ходаковская', N'8987389564', 1, 8, 3),
(N'Светлана', N'Тихонова', N'8987389864', 1, 9, 3),
(N'Иван3', N'Иванов3', N'89003891234', 2, 3, 4),
(N'Петр', N'Сидоров', N'89873007025', 2, 13, 4),
(N'Петр', N'Мигунов', N'89873070059', 2, 14, 4),
(N'Иван4', N'Иванов4', N'89765432891', 2, 4, 5),
(N'Иван5', N'Иванов5', N'89765432000', 3, 10, 6),
(N'Иван6', N'Иванов6', N'89765432800', 3, 11, 6),
(N'Иван7', N'Иванов7', N'89765432823', 3, 12, 6)
SELECT*FROM Employee