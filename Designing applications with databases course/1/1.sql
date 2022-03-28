/* Zad 1 */
SELECT DISTINCT SalesLT.Address.City FROM SalesLT.SalesOrderHeader 
JOIN SalesLT.Address ON SalesLT.SalesOrderHeader.ShipToAddressID = SalesLT.Address.AddressID
WHERE SalesLT.SalesOrderHeader.DueDate < GETDATE()
ORDER BY City;

/* Utworzyć zapytanie, które na podstawie SalesOrderHeader.ShipToAddressID zwróci listę miast, do których
towary zostały już dostarczone. Lista ma być posortowana i bez powtarzających się wartości.

Łączę tabele SalesOrderHeader oraz Adress. Wybieram tylko te wpisy z nazwami miast, 
których data realizacji (DueDate) jest mniejsza (wcześniejsza) niż obecna data z funkcji GETDATE
 */
