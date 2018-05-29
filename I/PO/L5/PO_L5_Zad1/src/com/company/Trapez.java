// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z1, Kolekcja figur
// PO_L5_Zad1
// Trapez.java
// 2018-04-01

package com.company;

public class Trapez extends Figura implements Comparable<Figura> {

    //Konstruktor domyślny trapezu.
    //Wywołuje konstruktor domyślny klasy Figura,
    //aby wyzerować wszystkie odziedziczone pola.
    //Następnie ustawia za pomocą mutatorów (metody get)
    //wartości odpowiednich dla klasy pól
    //(w przypadku trapezu są to oba boki oraz wysokość)
    // na wartość domyślną - 1.
    public Trapez() {
        super();
        setBok1(1);
        setBok2(1);
        setWysokosc(1);
    }

    //Konstruktor trapezu z parametrami.
    //Wywołuje konstruktor domyślny klasy Figura,
    //aby wyzerować wszystkie odziedziczone pola.
    //Następnie ustawia za pomocą mutatorów (metody get)
    //wartości odpowiednich dla klasy pól
    //(w przypadku trapezu są to oba boki oraz wysokość).
    public Trapez(float a, float b, float h) {
        super();
        setBok1(a);
        setBok2(b);
        setWysokosc(h);
    }
}