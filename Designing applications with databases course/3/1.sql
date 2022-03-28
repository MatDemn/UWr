-- Deklaracje struktur
DROP TABLE IF EXISTS Towary, Ceny, Kursy;
DROP FUNCTION IF EXISTS policzTowary, policzKursy;
GO
CREATE TABLE Towary(ID INT NOT NULL, NazwaTowaru VARCHAR(200) NOT NULL,
PRIMARY KEY (ID));
CREATE TABLE Kursy(Waluta VARCHAR(200) NOT NULL, CenaPLN DECIMAL(10,2),
PRIMARY KEY (Waluta));
CREATE TABLE Ceny(TowarID INT NOT NULL, Waluta VARCHAR(200) NOT NULL, Cena DECIMAL(10,2),
FOREIGN KEY (TowarID) REFERENCES Towary(ID),
FOREIGN KEY (Waluta) REFERENCES Kursy(Waluta));
GO

CREATE FUNCTION policzTowary() RETURNS INT
BEGIN
	DECLARE @ILE INT = 0;
	SELECT @ILE = COUNT(*) FROM Towary;
	RETURN @ILE;
END
GO

CREATE FUNCTION policzKursy() RETURNS INT
BEGIN
	DECLARE @ILE INT = 0;
	SELECT @ILE = COUNT(*) FROM Kursy;
	RETURN @ILE;
END
GO

--Wype³nianie struktur
DECLARE @i INT = 1;
WHILE(@i < 60)
BEGIN
	INSERT INTO Towary VALUES
	(@i, 'Towar'+cast(@i AS VARCHAR));
	SET @i = @i + 1
END

INSERT INTO Kursy VALUES
('Dolar', 4.07), ('Euro', 4.55), ('Bitcoin', 24986.58), ('Funt', 5.07), ('Rubel', 0.052), ('Frank', 4.28), ('Jen', 0.038),
('Korona', 0.17), ('Kuna', 0.6), ('Peso', 0.17), ('Z³oty', 1.0), 
('TEST', 0.0);


SET @i = 1;
DECLARE @ileKursow INT, @ileTowarow INT
SELECT @ileKursow = dbo.policzKursy()
SELECT @ileTowarow = dbo.policzTowary()

WHILE(@i < @ileTowarow)
BEGIN
	INSERT INTO Ceny VALUES
	(@i, 'Z³oty', CAST(RAND(CHECKSUM(NEWID())) * 10 as DECIMAL(10,2)) + 1);
	SET @i = @i + 1
END
GO


INSERT INTO Ceny VALUES
	(1, 'Dolar', 99.0),(1, 'Funt', 99.0), (1, 'Kuna', 99.0), 
	(2, 'Dolar', 99.0),(2, 'Funt', 99.0), (2, 'Kuna', 99.0), 
	(3, 'Dolar', 99.0),(3, 'Funt', 99.0), (3, 'Kuna', 99.0), 
	(4, 'Dolar', 99.0),(4, 'Funt', 99.0), (4, 'Kuna', 99.0),
	(5, 'Dolar', 99.0),(5, 'Funt', 99.0), (5, 'Kuna', 99.0),
	(6, 'Test', 99.0),(6, 'Test', 99.0), (6, 'Test', 99.0)


DECLARE CCeny CURSOR FOR SELECT TowarID, Waluta FROM Ceny -- Deklaracja kursora
DECLARE @ID INT, @waluta VARCHAR(200), @cenazl DECIMAL(10,2), @mnoznik DECIMAL(10,2) -- Deklaracje zmiennych przechowujacych wartosci
OPEN CCeny; -- Otwarcie kursora

FETCH NEXT FROM CCeny INTO @ID, @waluta -- Pobranie wartoœci kursora dla pêtli
WHILE (@@fetch_status=0)
BEGIN
	IF(@waluta != 'Zloty')
	BEGIN
		SELECT @cenazl = Cena FROM Ceny WHERE TowarID = @ID AND Waluta = 'Zloty' -- Pobieram wartosc w zlotych
		SELECT @mnoznik = CenaPLN FROM Kursy WHERE Waluta = @waluta;
		IF(@mnoznik != 0.0)
			BEGIN
			UPDATE Ceny 
			SET Cena = @cenazl*@mnoznik
			WHERE TowarID = @ID AND Waluta = @waluta
		END
	END
	FETCH NEXT FROM CCeny into @ID, @waluta
END
CLOSE CCeny
DEALLOCATE CCeny
GO
