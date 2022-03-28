// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z2, Wyrażenia arytmetyczne
// PO_L5_Zad2
// Main.java
// 2018-04-01

package com.company;
import java.util.HashMap;

public class Main {

    public static void main(String[] args) {
        System.out.println(">>Definiuję tablicę hashującą...");
        HashMap<String, Integer> varValues = new HashMap<>();

        System.out.println(">>Dodaję wartości zmiennych do tablicy: ");
        System.out.println("x: 15, y: 42, z: 20, a: 0");
        varValues.put("x", 15);
        varValues.put("y", 2);
        varValues.put("z", 20);
        varValues.put("a", 0);

        System.out.println(">>Definiuję jedną stałą oraz jedną zmienną: ");
        System.out.println("Stała: 5, zmienna: y");
        System.out.println("Zmienna korzysta " +
                "z zadeklarowanej wcześniej pozycji w tablicy: y");
        Stala stal = new Stala(5);
        Zmienna zm = new Zmienna("y", varValues);

        System.out.println("Wypisanie stałej (5) i jej obliczenie: ");
        System.out.println(stal);
        System.out.println(stal.oblicz());

        System.out.println("Wypisanie zmiennej (y) i jej obliczenie: ");
        System.out.println(zm);
        System.out.println(zm.oblicz());

        System.out.println("---------");
        System.out.println(">>Definiuję obiekt klasy Add z parametrami: " +
                "Stala(15) oraz Zmienna(y)");
        Add plusWyr = new Add(new Stala(15),
                new Zmienna("y", varValues));
        System.out.println("Wypisanie wyrazenia (plusWyr) " +
                "i jego obliczenie: ");
        System.out.println(plusWyr);
        System.out.println(plusWyr.oblicz());

        System.out.println("---------");
        System.out.println(">>Definiuję obiekt klasy Sub z parametrami: " +
                "Stala(42) oraz Zmienna(x)");
        Sub minusWyr = new Sub(new Stala(42),
                new Zmienna("x", varValues));
        System.out.println("Wypisanie wyrazenia (minusWyr) " +
                "i jego obliczenie: ");
        System.out.println(minusWyr);
        System.out.println(minusWyr.oblicz());

        System.out.println("---------");
        System.out.println(">>Definiuję obiekt klasy Mul z parametrami: " +
                "Stala(50) oraz Zmienna(z)");
        Mul mulWyr = new Mul(new Stala(50),
                new Zmienna("z", varValues));
        System.out.println("Wypisanie wyrazenia (mulWyr) " +
                "i jego obliczenie: ");
        System.out.println(mulWyr);
        System.out.println(mulWyr.oblicz());

        System.out.println("---------");
        System.out.println(">>Definiuję obiekt klasy Div z parametrami: " +
                "Stala(70) oraz Zmienna(a)");
        Div divWyr = new Div(new Stala(70),
                new Zmienna("a", varValues));
        System.out.println("Wypisanie wyrazenia (divWyr) " +
                "i jego obliczenie: ");
        System.out.println(divWyr);
        System.out.println(divWyr.oblicz());

        System.out.println(">>A teraz z poprawnym mianownikiem, zmienną y: ");
        System.out.println("---------");
        System.out.println(">>Definiuję obiekt klasy Div z parametrami: " +
                "Stala(70) oraz Zmienna(y)");
        Div div1Wyr = new Div(new Stala(70),
                new Zmienna("y", varValues));
        System.out.println("Wypisanie wyrazenia (div1Wyr) " +
                "i jego obliczenie: ");
        System.out.println(div1Wyr);
        System.out.println(div1Wyr.oblicz());

        System.out.println("Próba stworzenia obiektu klasy Zmienna za pomocą " +
                "niezdefiniowanej w tablicy haszującej zmiennej: h.");
        Zmienna nzmienna = new Zmienna("h", varValues);

/*

        System.out.println("2: "+ plusWyr1);
        System.out.println("3: "+ plusWyr.oblicz());
        System.out.println("4: "+ plusWyr1.oblicz());
        System.out.println("5: "+ zm.oblicz());*/
    }
}
