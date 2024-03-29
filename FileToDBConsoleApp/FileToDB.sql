USE [master]
GO
CREATE DATABASE [FileToDB]
go
use FileToDB
go
CREATE TABLE [dbo].[article](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[title] [nvarchar](max) NULL,
	[content] [nvarchar](max) NULL,
	[cat] [int] NULL,
)
CREATE TABLE [dbo].[cat](
	[id] [int] IDENTITY(1,1) primary key NOT NULL,
	[name] [nvarchar](max) NULL,
)
GO
USE [master]
GO

