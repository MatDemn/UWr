console.log((![]+[])[+[]]);
console.log((![]+[])[+!+[]]);
console.log(([![]]+[][[]])[+!+[]+[+[]]]);
console.log((![]+[])[!+[]+!+[]]);
/* 

    Podzieliłem wyrażenie na 4 części, aby łatwiej opisywać każde z nich. Mamy tutaj wypisane po prostu 4 litery f a i l.
    To co łączyło litery w pierwotnym wyrażeniu to operator +, który konkatenuje napisy. 
    Każde z tych wyrażeń ma taką samą strukturę:
    (coś)[coś]
    Wyrażenia w nawiasach okrągłych definiują nam typ wyrażenia (np. string, boolean). 
    Wyrażenia w nawiasach kwadratowych określają nam do jakiego indeksu tego wyrażenia się odnosimy.

    (![]+[])[+[]]

    1. Wyrażenie w nawiasach okrągłych: 
        [] to pusta tablica. Dodajemy przed nią !, co rzutuje nam całość na wartość boolean. 
        [] zwraca wartość true, więc dodanie ! sprawi, że będziemy mieli wartość false.
        Potem używamy operatora + i obok dopisujemy pustą tablicę ([]). 
        Dodanie wartości boolean oraz pustej tablicy rzutuje nam całość na typ string.
        Więc mamy string "false".
      Wyrażenie w nawiasach kwadratowych:
        Wiemy, że użycie + jako operatora unarnego to konwersja na liczbę. 
        Jeśli konwertujemy pustą tablicę na liczbę, otrzymujemy wartość 0.
      Tak więc finalnie mamy odczytanie elementu o indeksie 0 zze stringa "false", o daje literę "f".
    
    (![]+[])[+!+[]]

    2. Tutaj sytuacja jest podobna do poprzedniej, różni się tylko wyrażenie w nawiasach kwadratowych.
        Wyrażenie w nawiasach kwadratowych:
        Negacja nie ma tutaj argumentu, więc negowana jest wartość 0. Po negacji 0 otrzumujemy boolean: true.
        Operator + konwertuje nam to na numer: 1. Póżniej mamy dodanie tej 1 do pustej tablicy, 
        która po rzutowaniu na numer daje 0. 
        Więc otrzymujemy:
        +!(0) + [] -> 1 + 0 -> 1
        Czyli odczytujemy element o indeksie 1 z takiego samego stringa co poprzednio (false). Otrzymujemy literę 'a'.

    ([![]]+[][[]])[+!+[]+[+[]]]

    3. Wyrażenie w nawiasach okrągłych: 
        Negacja pustej tablicy daje wartość false w boolean. Potem opakowujemy tę wartość false w tablicę.
        Mamy w takim razie [false]. Wyrażenie po operatorze + to wzięcie z pustej tablicy elementu o indeksie
        "". Mamy taką strukturę: [][x], gdzie w miejscu x musimy dokonać jakiejś konwersji na string,
        aby móc odczytać jakieś pole w tej tablicy (która jest obiektem). Operacja toString na pustej tablicy
        daje nam po prostu "". Nie ma takiego pola w obiekcie [], więc dostajemy undefined.
        Na końcu dodajemy [false] do undefined. To nam konwertuje całość na string.
        Więc wykonujemy toString na obu elementach, także [false] staje się "false",
        za to undefined staje się "undefined". Łączymy napisy, co daje nam napis "falseundefined".
      Wyrażenie w nawiasach kwadratowych:
        Robimy negację zera i konwertujemy to na liczbę: +!(0) -> 1, potem dodajemy to do pustej tablicy,
        co konwertuje całość na obiekt. Tak powstałe wyrażenie dodajemy do tablicy zawierającej element 0
        (bo +[] konwertuje pustą tablicę na numer, czyli 0). Czyli dodajemy obiekty, a to konwertuje oba na
        string i łączy napisy. Czyli mamy 10.
      Tak więc finalnie
        Odczytujemy element o indeksie 10 ze stringa "falseundefined", czyli literę "i".

    (![]+[])[!+[]+!+[]]

    4. Wyrażenie w nawiasach okrągłych: 
        Tak samo jak w przypadku 1 i 2, dostajemy string "false".
      Wyrażenie w nawiasach kwadratowych:
        !+[] daje nam liczbę 1 (negacja 0 daje 1, potem dodanie boolean do obiektu daje numer 1).
        Dodajemy takie wyrażenie do drugiego, identycznego. Mamy dodawanie liczb, czyli 1+1 = 2.
      Tak więc finalnie
        Odczytujemy element o indeksie 2 z napisu "false", czyli "l".

*/

