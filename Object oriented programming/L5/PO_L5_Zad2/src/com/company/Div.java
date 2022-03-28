// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z2, Wyrażenia arytmetyczne
// PO_L5_Zad2
// Div.java
// 2018-04-01

package com.company;

public class Div extends Wyrazenie {

    //Konstruktor klasy. Buduje obiekt Div za pomocą dwóch obiektów.
    public Div(Wyrazenie a, Wyrazenie b) {
        super(a, b);
    }


    //Metoda pomocnicza dla metody oblicz().
    // Ma za zadanie sprawdzać czy prawa strona wyrażenia nie jest równa zeru
    // i wyrzucać wyjątek, jeśli to nastąpi.
    private int pomOblicz() {
        if(getRight().oblicz() == 0) {
            throw new IllegalArgumentException("DZIELENIE PRZEZ ZERO!");
        }
        else
            return getLeft().oblicz() / getRight().oblicz();
    }


    //Metoda wyliczająca wartość ilorazu lewej i prawej strony.
    //Obsługuje wyjątki zwracane przez metodę pomOblicz().
    //W przypadku dzielenia przez zero zwraca lewą stronę wyrażenia.
    public int oblicz() {
        try {
            pomOblicz();
        }
        catch(IllegalArgumentException e) {
            System.out.println("DZIELENIE PRZEZ ZERO! " +
                    "ZWRACAM OBLICZONĄ LEWĄ STRONĘ: ");
            return getLeft().oblicz();
        }
        return pomOblicz();
    }


    //Metoda definiująca sposób wyświetlenia obiektu klasy na ekranie.
    public String toString() {
        return "( " + getLeft() + " / " + getRight() + " )";
    }

}
