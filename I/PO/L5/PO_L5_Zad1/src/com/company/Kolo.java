// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z1, Kolekcja figur
// PO_L5_Zad1
// Kolo.java
// 2018-04-01

package com.company;

public class Kolo extends Figura implements Comparable<Figura> {

    //Konstruktor domyślny koła.
    //Wywołuje konstruktor domyślny klasy Figura,
    //aby wyzerować wszystkie odziedziczone pola.
    //Następnie ustawia za pomocą mutatorów (metody get)
    //wartości odpowiednich dla klasy pól
    //(w przypadku koła jest to promień)
    // na wartość domyślną - 1.
    public Kolo() {
        super();
        setPromien(1);
    }


    //Konstruktor koła z parametrami.
    //Wywołuje konstruktor domyślny klasy Figura,
    //aby wyzerować wszystkie odziedziczone pola.
    //Następnie ustawia za pomocą mutatorów (metody get)
    //wartości odpowiednich dla klasy pól
    //(w przypadku koła jest to promień).
    public Kolo(float a) {
        super();
        setPromien(a);
    }
}
