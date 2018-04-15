package com.company;

public class Main {

    public static void main(String[] args) {

        Object ob1;
        //0 - nazwa pliku; 1 - nazwa klasy
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
        catch(ArrayIndexOutOfBoundsException e) {
            System.out.println("NOT ENOUGH ARGUMENTS! " +
                    "NEEDED 2!");
        }
    }
}
