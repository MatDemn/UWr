package com.company;

public class Main {

    public static void main(String[] args) {

        Object ob1; //deklaruję obiekt klasy Object
        // (gdyż nie wiem na tym etapie programu która z klas będzie
        // odczytana z pliku)

        // indeksy w args: 0 - nazwa pliku; 1 - nazwa klasy

        // Jeżeli w argumencie wywołania podano Trojkat,
        // dostosowuję wcześniej utworzony obiekt do tej klasy.
        // Analogicznie w przypadku klasy Okrag.
        // Jeżeli podano jakąs inną (nieznaną programowi) klasę,
        // sygnalizuję to wypisaniem w konsoli informacji.
        try{
            if(args[1].equals("Trojkat")) {
                ob1 = new Trojkat("#NO_COLOR", "#NO_NAME", 0,0);
                ((Trojkat) ob1).guiFig(args[0]);
            }
            else if(args[1].equals("Okrag")) {
                ob1 = new Okrag("#NO_COLOR", "#NO_NAME", 0);
                ((Okrag) ob1).guiFig(args[0]);
            }
            else {
                System.out.println("CAN'T RECOGNIZE SUCH OBJECT!");
            }
        }
        // Jeżeli nie podano przynajmniej 2 argumentów.
        catch(ArrayIndexOutOfBoundsException e) {
            System.out.println("NOT ENOUGH ARGUMENTS! " +
                    "NEEDED 2!");
        }
    }
}
