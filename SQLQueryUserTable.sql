create database UserSignUpDB

go

use UserSignUpDB

create table UserInfo
(
UserId int identity PRIMARY KEY,
Username VARCHAR(20),
FirstName VARCHAR(20),
LastName VARCHAR(20),
Email VARCHAR(255),
Password VARCHAR(20)
)