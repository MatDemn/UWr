// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z1, Kolekcja figur
// PO_L5_Zad1
// Main.java
// 2018-04-01

package com.company;

public class Main {

    public static void main(String[] args) {
        System.out.println(">>Definiuję obiekty każdej z klas...");
        Prostokat fig1 = new Prostokat(10, 10);
        Kolo fig2 = new Kolo(20);
        Trojkat fig3 = new Trojkat(30, 40);
        Trapez fig4 = new Trapez(50, 60, 70);
        System.out.println(">>Zadeklarowano: ");
        System.out.println(" -Fig1: " + fig1 + " o polu: " + fig1.getPole());
        System.out.println(" -Fig2: " + fig2 + " o polu: " + fig2.getPole());
        System.out.println(" -Fig3: " + fig3 + " o polu: " + fig3.getPole());
        System.out.println(" -Fig4: " + fig4 + " o polu: " + fig4.getPole());

        System.out.println(">>Czy fig2 < fig1 (pod względem pola)?");
        System.out.println(">>Porównanie za pomocą metody compareTo");
        System.out.println(fig2.compareTo(fig1));

        System.out.println(">>Definiuję nową kolekcję figur...");
        FigCollection koll = new FigCollection();

        System.out.println("Dodaję fig1...");
        koll.addElem(fig1);
        koll.wypisz();
        System.out.println("---------------");
        System.out.println("Dodaję fig2...");
        koll.addElem(fig2);
        koll.wypisz();
        System.out.println("---------------");
        System.out.println("Dodaję fig3...");
        koll.addElem(fig3);
        koll.wypisz();
        System.out.println("---------------");
        System.out.println("Dodaję fig4...");
        koll.addElem(fig4);
        koll.wypisz();
        System.out.println("---------------");

        System.out.println("Jak widzimy, figury zostały " +
                "dodane według ich pól powierzchni.");
    }
}
