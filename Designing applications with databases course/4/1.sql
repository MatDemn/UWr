-- Brudne dane - Pomi�dzy zapytaniami w tej samej transakcji odczytujemy r�ne dane. 
-- Tutaj mamy r�ne wyniki tego samego zapytania
S1:
BEGIN TRAN
UPDATE liczby
   SET liczba = 42

S2: 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SELECT * FROM liczby

S1: 
ROLLBACK TRAN

S2:
SELECT * FROM liczby

--		Pojawi�y si� r�ne dane

-- Niepowtarzalno�� odczyt�w - Robimy podobnie co wcze�niej, tylko teraz pomi�dzy selectami
-- licz�cymi �redni� mamy zmian� jednego pola, co powoduje zmian� �redniej w drugim select
S1:
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRAN
SELECT AVG(liczba) from liczby

S2:
UPDATE liczby
   SET liczba = 500 WHERE liczba = 151

S1:
SELECT AVG(liczba) from liczby

		-- Jest ca�kowicie inna �rednia pomi�dzy Selectami

-- Odczyty fantom�w - Nowa pozycja pojawia si� w drugim select
S1:
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
SELECT * FROM liczby
   WHERE liczba BETWEEN 20 AND 40

S2:
INSERT INTO liczby
   VALUES (999)

S1:
SELECT * FROM liczby
   WHERE liczba BETWEEN 20 AND 40

-- W drugim SELECT pojawi�a si� nowa pozycja, kt�rej nie by�o wcze�niej

-- Problemy, ktore napotykamy i ich rozwi�zania:
-- Brudne dane (rozwi�zanie: READ COMMITTED)
-- Niepowtarzalno�� odczyt�w (rozwi�zanie: REPEATABLE READ)
-- Fantomy (rozwi�zanie: SERIALIZABLE)

-- Im wy�szy poziom izolacji, tym wi�cej jest blokowane w bazie danych podczas transakcji
