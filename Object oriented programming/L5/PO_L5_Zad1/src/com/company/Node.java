// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z1, Kolekcja figur
// PO_L5_Zad1
// Node.java
// 2018-04-01

package com.company;

public class Node {

    //Pola klasy
    private Figura fig;
    private Node next;


    //Akcesory i mutatory
    public Figura getFig() {return fig;}
    public Node getNext() {return next;}
    public void setFig(Figura val) {fig = val;}
    public void setNext(Node val) {next = val;}


    //Konstruktor domyślny Node. Tworzy obiekt z pustymi polami.
    public Node() {
        fig = null;
        next = null;
    }


    //Konstruktor z parametrem Figura.
    // Tworzy nowy obiekt i dodaje do niego Figurę podaną w argumencie.
    public Node(Figura temp) {
        fig = temp;
        next = null;
    }

    //Definiuje sposób, w jaki zostanie wydrukowany obiekt
    // za pomocą poleceń takich jak System.out.println();
    public String toString() {
        return "Node: " + getFig();
    }

}
