// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z1, Kolekcja figur
// PO_L5_Zad1
// Trojkat.java
// 2018-04-01

package com.company;

public class Trojkat extends Figura implements Comparable<Figura> {

    //Konstruktor domyślny trójkąta.
    //Wywołuje konstruktor domyślny klasy Figura,
    //aby wyzerować wszystkie odziedziczone pola.
    //Następnie ustawia za pomocą mutatorów (metody get)
    //wartości odpowiednich dla klasy pól
    //(w przypadku trójkąta jest to bok oraz wysokość)
    // na wartość domyślną - 1.
    public Trojkat() {
        super();
        setWysokosc(1);
        setBok1(1);
    }


    //Konstruktor trójkąta z parametrami.
    //Wywołuje konstruktor domyślny klasy Figura,
    //aby wyzerować wszystkie odziedziczone pola.
    //Następnie ustawia za pomocą mutatorów (metody get)
    //wartości odpowiednich dla klasy pól
    //(w przypadku trójkąta jest to bok oraz wysokość).
    public Trojkat(float a, float b) {
        super();
        setWysokosc(b);
        setBok1(a);
    }
}
