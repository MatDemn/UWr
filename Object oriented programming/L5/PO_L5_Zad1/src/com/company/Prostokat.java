// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z1, Kolekcja figur
// PO_L5_Zad1
// Prostokat.java
// 2018-04-01

package com.company;

public class Prostokat extends Figura implements Comparable<Figura> {

    //Konstruktor domyślny prostokąta.
    //Wywołuje konstruktor domyślny klasy Figura,
    //aby wyzerować wszystkie odziedziczone pola.
    //Następnie ustawia za pomocą mutatorów (metody get)
    //wartości odpowiednich dla klasy pól
    //(w przypadku prostokąta są to oba boki)
    //na wartość domyślną - 1.
    public Prostokat() {
        super();
        setBok1(1);
        setBok2(1);
    }


    //Konstruktor prostokąta z parametrami.
    //Wywołuje konstruktor domyślny klasy Figura,
    //aby wyzerować wszystkie odziedziczone pola.
    //Następnie ustawia za pomocą mutatorów (metody get)
    //wartości odpowiednich dla klasy pól
    //(w przypadku trapezu są to oba boki).
    public Prostokat(float a, float b) {
        super();
        setBok1(a);
        setBok2(b);
    }
}
