// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z1, Kolekcja figur
// PO_L5_Zad1
// FigCollection.java
// 2018-04-01

package com.company;

public class FigCollection {
    //Pola klasy
    private Node elem;


    //Konstruktor domyślny
    public FigCollection(){
        elem = null;
    }


    //Konstruktor tworzący obiekt na podstawie figury.
    public FigCollection(Figura fig){
        elem = new Node(fig);
    }


    //Akcesory i mutatory
    public Node getElem() { return elem;}
    public void setElem(Node val) {elem = val;}


    //Metoda usuwająca figurę z kolekcji
    public Figura removeElem() {
        Figura temp = elem.getFig();
        elem = elem.getNext();
        return temp;
    }


    //Metoda dodająca figurę do kolekcji, z zachowaniem porządku
    public void addElem(Figura temp) {
        Node copy = elem;
        if(elem == null) {
            elem = new Node(temp);
        }
        else {
            while(elem.getNext() != null) {
                if (elem.getNext().getFig().compareTo(temp) == 1) {
                    Node nowy = new Node(temp);
                    nowy.setNext(elem.getNext());
                    elem.setNext(nowy);
                    setElem(nowy);
                    elem = copy;
                    break;
                } else {
                    elem = elem.getNext();
                }
            }
            if (elem.getNext() == null) {
                elem.setNext(new Node(temp));
                elem = copy;
            }

        }
    }


    //Metoda wypisująca całą kolekcję
    public void wypisz() {
        Node copy = elem;
        int iterator = 1;
        while(elem!=null) {
            System.out.println("Zawartość kolekcji: ");
            System.out.println(iterator + " pole: "+ elem.getFig().getPole()+ " " + getElem());
            iterator += 1;
            elem = elem.getNext();
        }
        elem = copy;
    }
}
