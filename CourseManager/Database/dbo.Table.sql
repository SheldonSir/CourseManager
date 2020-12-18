CREATE TABLE [dbo].[Table]
(
	Id varchar(20) NOT NULL PRIMARY KEY,
	Name varchar(20),
	RealName varchar(20),
	Password varchar(20) NOT NULL,
	IsValidation int,
	CanLogin int,
	IsTeacher int,
	Avatar  varchar(200),
	Gender int
)
