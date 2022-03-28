// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z2, Wyrażenia arytmetyczne
// PO_L5_Zad2
// Stala.java
// 2018-04-01

package com.company;

public class Stala extends Wyrazenie {

    //Pola klasy
    private int value;


    //Konstruktor klasy.
    public Stala(int val) {value = val;}


    //Wylicza wartość stałej. Czyli zwraca wartość pola value.
    public int oblicz() {
        return value;
    }


    //Metoda definiująca sposób wyświetlenia obiektu klasy na ekranie.
    public String toString() {
        return "" + value;
    }
}
