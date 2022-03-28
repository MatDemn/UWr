USE [WEPPO_8]
GO

/****** Object:  Table [dbo].[Osoba]    Script Date: 14.01.2021 12:18:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Osoba](
	[ID] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Imie] [nvarchar](20) NOT NULL,
	[Nazwisko] [nvarchar](20) NOT NULL,
	[Plec] [nvarchar](20) NOT NULL,
	[MiejsceZamieszkania] [nvarchar](50) NOT NULL,
	[DataUrodzenia] [date] NOT NULL,
	[Telefon] [nvarchar](20) NOT NULL)


