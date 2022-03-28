// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z2, Wyrażenia arytmetyczne
// PO_L5_Zad2
// Sub.java
// 2018-04-01

package com.company;

public class Sub extends Wyrazenie {

    //Konstruktor klasy. Buduje obiekt Sub za pomocą dwóch obiektów.
    public Sub(Wyrazenie a, Wyrazenie b) {
        super(a, b);
    }


    //Metoda wyliczająca wartość różnicy lewej i prawej strony.
    public int oblicz() {
        return getLeft().oblicz() - getRight().oblicz();
    }


    //Metoda definiująca sposób wyświetlenia obiektu klasy na ekranie.
    public String toString() {
        return "( " + getLeft() + " - " + getRight() + " )";
    }

}