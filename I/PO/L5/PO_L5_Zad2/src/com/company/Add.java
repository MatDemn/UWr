// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z2, Wyrażenia arytmetyczne
// PO_L5_Zad2
// Add.java
// 2018-04-01

package com.company;

public class Add extends Wyrazenie {

    //Konstruktor klasy. Buduje obiekt Add za pomocą dwóch obiektów.
    public Add(Wyrazenie a, Wyrazenie b) {
        super(a, b);
    }


    //Metoda wyliczająca wartość sumy lewej i prawej strony.
    public int oblicz() {
        return getLeft().oblicz() + getRight().oblicz();
    }


    //Metoda definiująca sposób wyświetlenia obiektu klasy na ekranie.
    public String toString() {
        return "( " + getLeft() + " + " + getRight() + " )";
    }

}
