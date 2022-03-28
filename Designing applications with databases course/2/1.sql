-- ZAD 1 --

drop function if exists przetrzymywaneKsiazki
GO

create function przetrzymywaneKsiazki(@ile int) returns table
	return (SELECT PESEL, COUNT(PESEL) ILOSC FROM dbo.Wypozyczenie w JOIN dbo.Czytelnik c ON w.Czytelnik_ID = c.Czytelnik_ID
			WHERE w.Liczba_Dni > @ile
			GROUP BY PESEL)
go

select * from przetrzymywaneKsiazki(9);
go

