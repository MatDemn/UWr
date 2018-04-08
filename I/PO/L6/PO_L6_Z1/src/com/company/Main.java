package com.company;

import java.io.*;

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

        System.out.println(">>KONIEC TESTÓW");

    }
}
