package com.company;

import java.io.*;
import java.util.ArrayList;
import java.util.ArrayList;
import java.util.List;

public class Main {

    public static void main(String[] args) throws IOException, ClassNotFoundException{
        Lista<Integer> MyList = new Lista<>();

        System.out.println(">>Wynik działania metody IsEmpty: ");
        System.out.println(MyList.IsEmpty());

        System.out.println(">>Dodaję na początek listy elementy:" +
                " 3, 2, 1 (w kolejności występowania)");
        MyList.AddBegin(3);
        MyList.AddBegin(2);
        MyList.AddBegin(1);

        System.out.println(">>Wynik działania metody IsEmpty po dodaniu: ");
        System.out.println(MyList.IsEmpty());

        System.out.println(">>Przechodzę po wszystkich elementach: ");

        MyList.printList();

        System.out.println(">>Dodaję na koniec listy elementy: " +
                "4, 5, 6 (w kolejności występowania)");
        MyList.AddEnd(4);
        MyList.AddEnd(5);
        MyList.AddEnd(6);

        System.out.println(">>Przechodzę po wszystkich elementach: ");

        MyList.printList();

        System.out.println(">>Usuwam jeden element z początku " +
                "i jeden z końca listy " +
                "oraz wypisuję elementy listy");


        MyList.DelBegin();
        MyList.DelEnd();

        MyList.printList();

        System.out.println(">>Na liście są 4 elementy, " +
                "usuwam je wszystkie " +
                "i wywołuję metodę IsEmpty:");
        MyList.DelBegin();
        MyList.DelBegin();
        MyList.DelBegin();
        MyList.DelBegin();
        System.out.println(">>Wynik IsEmpty:");
        System.out.println(MyList.IsEmpty());
        System.out.println(">>Metoda zwróciła true, " +
                "więc poprawnie usunięto elementy listy.");


        System.out.println();
        System.out.println();
        System.out.println(">>POCZĄTEK TESTÓW LISTY 6");
        System.out.println("----------TESTY INTERFEJSU" +
                " SERIALIZABLE----------");

        System.out.println(">>Dodaję elementy: 12, 33, 42, 15, 46" +
                " na koniec listy.");
        MyList.AddEnd(12);
        MyList.AddEnd(33);
        MyList.AddEnd(42);
        MyList.AddEnd(15);
        MyList.AddEnd(46);
        System.out.println(">>Używam serializacji do zapisu na dysku " +
                "obiektu MyList, który właśnie zmodyfikowałem.");

        try (ObjectOutputStream outputStream = new ObjectOutputStream(new FileOutputStream("myList.bin"))) {
            outputStream.writeObject(MyList);
        }

        System.out.println(">>Teraz zmodyfikuję listę. Usunę 2 elementy z końca " +
                "(46, 15) i dodam 2 na początek (99, 100)");
        MyList.DelEnd();
        MyList.DelEnd();
        MyList.AddBegin(99);
        MyList.AddBegin(100);

        System.out.println(">>Odczytuję obiekt z dysku " +
                "i wypisuję zawartość pól: ");

        try (ObjectInputStream input = new ObjectInputStream(new FileInputStream("myList.bin"))) {
            Lista readLista = (Lista) input.readObject();
            readLista.printList();
        }

        System.out.println(">>Teraz wypisuję zawartość listy zmodyfikowanej: ");
        MyList.printList();

        System.out.println("----------TESTY INTERFEJSU COLLECTION----------");

        // ----------------------------------------------------

        System.out.println("W testach kolekcja 'MyList' to kolekcja, " +
                "którą testuję.");

        System.out.println(">>Tworzę listę 'toAdd', " +
                "jako listę elementów: 1, 22, 77, 89, 10");
        List<Integer> toAdd = new ArrayList<>();
        toAdd.add(1);
        toAdd.add(22);
        toAdd.add(77);
        toAdd.add(89);
        toAdd.add(10);

/*
        System.out.println(">>Tworzę listę 'toCompare'," +
                "jako listę elementów: 5, 42, 6, 7, 0");
        List<Integer> toCompare = new ArrayList<>();
        toCompare.add(5);
        toCompare.add(42);
        toCompare.add(6);
        toCompare.add(7);
        toCompare.add(0);
*/
        System.out.println(">>Używam metody clear, " +
                "aby usunąć wszystkie elementy z listy MyList.");
        MyList.clear();

        System.out.println(">>Używam metody isEmpty na MyList, " +
                "aby sprawdzić czy clear wyczyścił kolekcję: ");
        System.out.println(MyList.isEmpty());


        System.out.println(">>Dodaję do listy MyList: 5, 42, 24");
        MyList.add(5);
        MyList.add(42);
        MyList.add(24);


        System.out.println("Dodaję wszystkie elementy z listy toAdd " +
                "do MyList");
        MyList.addAll(toAdd);


        System.out.println("Lista 'secondMyList' ma teraz kilka elementów " +
                "z MyList, dzieki metodzie pomocniczej.");
        Lista secondMyList = new Lista(MyList);


        System.out.println("Czy pierwszy element " +
                "secondMyList jest w liście MyList?");
        System.out.println(MyList.contains(secondMyList.getElemP()));

        System.out.println("Przez brak implementacji metody iterator" +
                "poniższe metody nie działają poprawnie.");
        /*
        System.out.println("Czy każdy element z secondMyList jest w MyList?");
        System.out.println(MyList.containsAll(secondMyList));

        System.out.println("Usuwam z MyList pierwszy element secondMyList");
        MyList.remove(secondMyList.getElemP());

        System.out.println("Czy pierwszy element " +
                "secondMyList jest w liście MyList?");
        System.out.println(MyList.contains(secondMyList.getElemP()));

        /*
        System.out.println("Czy każdy element z secondMyList jest w MyList?");
        //System.out.println(MyList.containsAll(secondMyList));


        System.out.println("Usuwam wszystkie elementy z MyList, " +
                "które są obecne w toAdd");
        System.out.println("Przed operacją: ");
        MyList.printList();
        System.out.println("Po operacji: ");
        MyList.removeAll(toAdd);
        System.out.println("##############");
        MyList.printList();

        System.out.println("Używam metody retailAll " +
                "na MyList z argumentem toAdd.");
        System.out.println("Po wykonaniu metody MyList powinno być puste.");
        MyList.retainAll(toAdd);
        */

        System.out.println("Zawartość listy: ");
        MyList.printList();
        System.out.println("Ile jest elementów w MyList?");
        System.out.println(MyList.size());

        // ----------------------------------------------------
        System.out.println(">>KONIEC TESTÓW");

    }
}
