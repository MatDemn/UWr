// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z2, Wyrażenia arytmetyczne
// PO_L5_Zad2
// Zmienna.java
// 2018-04-01

package com.company;
import java.util.HashMap;

public class Zmienna extends Wyrazenie{

    //Pola klasy
    private String nazwa;
    private int value;


    //Konstruktor klasy.
    // Tworzy obiekt na podstawie podanego stringa i tablicy haszującej.
    //Do pola value przypisuje wartość znalezionego elementu w tablicy.
    //Jeżeli element o podanej nazwie nie zostanie znaleziony,
    // zostaje zwrócony wyjątek.
    public Zmienna(String name, HashMap<String, Integer> hmap) {
        nazwa = name;
        try {
            value = hmap.get(name);
        }
        catch(NullPointerException e) {
            System.out.println("ERROR. Value '" + nazwa +
                    "' is not declared in hashtable. Wyjątek: " + e);
            value = 0;
        }
    }


    //Metoda wyliczająca wartość wyrażenia.
    public int oblicz() {
        return value;
    }


    //Metoda definiująca sposób wyświetlenia obiektu klasy na ekranie.
    public String toString() {
        return nazwa;
    }
}
