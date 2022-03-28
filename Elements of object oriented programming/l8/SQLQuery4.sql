/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [ID]
      ,[Imie]
      ,[Nazwisko]
      ,[Plec]
      ,[MiejsceZamieszkania]
      ,[DataUrodzenia]
      ,[Telefon]
  FROM [WEPPO_8].[dbo].[Osoba]

DELETE FROM dbo.Osoba

ALTER SEQUENCE dbo.OsobaSeq RESTART

INSERT INTO dbo.Osoba (Imie, Nazwisko, Plec, MiejsceZamieszkania, DataUrodzenia, Telefon)
VALUES
('Jacek', 'Kowalski', 'M', 'Warszawa', '1997-10-13', '+48 543-123-111'),
('Anna', 'Manna', 'K', 'Wroc³aw', '1998-02-22', '+48 623-856-002'),
('Krzysztof', 'Alibaba', 'M', 'Poznañ', '2002-08-01', '+48 540-009-112'),
('Antoni', 'Makler', 'M', 'Gdañsk', '1960-01-01', '+48 720-101-482'),
('Katarzyna', 'Kobierska', 'K', 'Szczecin', '1990-03-07', '+48 824-105-518')

SELECT * FROM dbo.Osoba WHERE MiejsceZamieszkania >= 'A'