// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z2, Wyrażenia arytmetyczne
// PO_L5_Zad2
// Mul.java
// 2018-04-01

package com.company;

public class Mul extends Wyrazenie {

    //Konstruktor klasy. Buduje obiekt Mul za pomocą dwóch obiektów.
    public Mul(Wyrazenie a, Wyrazenie b) {
        super(a, b);
    }


    //Metoda wyliczająca wartość iloczynu lewej i prawej strony.
    public int oblicz() {
        return getLeft().oblicz() * getRight().oblicz();
    }


    //Metoda definiująca sposób wyświetlenia obiektu klasy na ekranie.
    public String toString() {
        return "( " + getLeft() + " * " + getRight() + " )";
    }

}
