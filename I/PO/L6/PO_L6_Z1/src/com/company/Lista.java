package com.company;

import jdk.jshell.spi.ExecutionControl;

import java.io.Serializable;
import java.util.Collection;
import java.util.Iterator;

public class Lista<T> implements Serializable, Collection<T> {

    private Node<T> elemP;
    private Node<T> elemK;

    public Node<T> getElemP() {return elemP;}
    public void setElemP(Node<T> val) {elemP = val;}

    public Lista() // konstruktor listy
    {
        elemP = null;
        elemK = null;
    }


    //Metoda stworzona tylko do testów!
    //Przyczepia wycinek listy do nowej, pustej listy
    public Lista(Lista<T> lis) {
        elemP = lis.elemP.getNext().getNext().getNext();
        elemK = lis.elemK;
    }

    public void AddBegin(T val)    // dodanie na początek listy
    {
        if (elemP == null) // kiedy lista jest pusta
        {

            elemP = new Node<T>();
            elemP.setValue(val);
            elemK = elemP;
        }
        else if (elemP == elemK) // kiedy jest dokładnie jeden element
        {
            Node<T> nowy = new Node<T>(); // tworzę nowy pojemnik
            nowy.setValue(val); // nadaję mu wartość
            elemP = nowy; // jest jeden element,
            //wiec chcę ten pierwszy element traktować jako nowy
            elemP.setNext(elemK); // przyczepiam P do glowy K
            elemK.setPrev(elemP); // robię referencję
            //do poprzedniego elementu, czyli P
        }
        else // kiedy jest więcej niż jeden element
        {
            Node<T> nowy = new Node<T>(); // tworze nowy pojemnik
            Node<T> temp = new Node<T>(); // tworze temp
            nowy.setValue(val); // nadaje mu wartosc
            temp = elemP; // zapisuje referencje do glowy aktualnej
            elemP = nowy; // podmieniam poczatek z nowym
            elemP.setNext(temp); // podpinam
            temp.setPrev(elemP);
        }

        System.out.println("#Metoda: Wykonałem AddBegin!");
    }

    public Node<T> DelBegin() // usunięcie z początku listy
    {
        if (elemP == null) // lista jest pusta
        {
            System.out.println("#Metoda: Niestety, lista jest pusta!");
            return elemP;
        }
        else if (elemP == elemK) // na liście jest dokładnie jeden element

        {
            Node<T> temp = new Node<T>();
            temp = elemP;
            elemP = elemP.getNext();
            elemK = elemK.getNext();
            System.out.println("#Metoda: Wykonałem DelBegin!");
            return temp;
        }
        else // na liście jest więcej niż jeden element
        {
            Node<T> temp = new Node<T>();
            temp = elemP;
            elemP = elemP.getNext();
            System.out.println("#Metoda: Wykonałem DelBegin!");
            return temp;
        }
    }

    public void AddEnd(T val) // dodanie na koniec listy
    {
        if (elemK == null) // lista jest pusta
        {

            elemK = new Node<T>();
            elemK.setValue(val);
            elemP = elemK;
        }
        else if (elemP == elemK) // lista ma jeden element
        {
            Node<T> nowy = new Node<T>();
            nowy.setValue(val);
            elemK = nowy;
            elemK.setPrev(elemP);
            elemP.setNext(elemK);
        }
        else // lista ma więcej niż jeden element
        {
            Node<T> nowy = new Node<T>();
            Node<T> temp = new Node<T>();
            nowy.setValue(val);
            temp = elemK;
            elemK = nowy;
            elemK.setPrev(temp);
            temp.setNext(elemK);
        }

        System.out.println("#Metoda: Wykonałem AddBegin!");
    }

    public Node<T> DelEnd() // usunięcie z końca listy
    {
        if (elemK == null) // lista jest pusta
        {
            System.out.println("#Metoda:Niestety, lista jest pusta!");
            System.out.println("#Metoda:Wykonałem DelEnd!");
            return elemK;
        }
        else if (elemP == elemK) // lista ma dokładnie jeden element

        {
            Node<T> temp = new Node<T>();
            temp = elemK;
            elemK = elemK.getPrev();
            elemP = elemP.getPrev();
            System.out.println("#Metoda:Wykonałem DelEnd!");
            return temp;
        }
        else // lista ma więcej niż jeden element
        {
            Node<T> temp = new Node<T>();
            temp = elemK;
            elemK = elemK.getPrev();
            elemK.setNext(null);
            System.out.println("#Metoda: Wykonałem DelEnd!");
            return temp;
        }
    }

    public boolean IsEmpty() // sprawdza czy lista jest pusta
    {
        if (elemP == null && elemK == null) return true;
        return false;
    }

    public T CheckP() // zwraca element z początku listy
    {
        if (elemP == null)
        {
            try {elemP.getValue();}
            catch(NullPointerException e) {
                System.out.println("#Metoda: UPS! Nie ma elementu! " +
                "Nie mogę zwrócić czegoś, czego nie ma.");}
        }
        return elemP.getValue();
    }

    public T CheckK() // zwraca element z końca listy
    {
        if (elemK == null)
        {
            try {elemP.getValue();}
            catch(NullPointerException e) {
                System.out.println("#Metoda: UPS! Nie ma elementu! " +
                        "Nie mogę zwrócić czegoś, czego nie ma.");}
        }
        return elemK.getValue();
    }

    public void N_elem() // przechodzi do kolejnego elementu
    {/*
        if(elemP.getNext() == null) {
            System.out.println("#Metoda: KONIEC LISTY");
        }*/
        elemP = elemP.getNext();
    }


    //Metoda wypisująca całą listę
    public void printList() {
        Node<T> temp = getElemP();

        while (getElemP() != null)
        {
            System.out.println(CheckP());
            N_elem();
        }

        setElemP(temp); // wracam do początku listy, zapisanej w temp
    }


    //Metoda licząca elementy w kolekcji
    public int howMany() {
        Node<T> temp;
        int wyn = 0;
        temp = elemP;
        while(elemP != null) {
            wyn += 1;
            N_elem();
        }
        elemP = temp;
        return wyn;
    }


    //Metoda niezbędna do implementacji interfejsu Collection<E>
    //Zwraca tablicę elementów kolekcji
    public Object[] toArray() {
        int howmany = howMany();
        int iter = 0;
        Node<T> temp = elemP;
        Object[] wyn = new Object[howmany];
        while(elemP != null) {
            wyn[iter] = elemP.getValue();
            iter += 1;
            N_elem();
        }
        elemP = temp;
        return wyn;
    }


    //Metoda niezbędna do implementacji interfejsu Collection<E>
    //Zwraca tablicę elementów tablicy array
    public Object[] toArray(Object array[]) {
        int howmany = array.length;
        int iter = 0;
        Object[] wyn = new Object[howmany];
        while(iter != howmany) {
            wyn[iter] = array[iter];
            iter += 1;
        }
        return wyn;
    }

    //------------------------------------------------------------------------
    //------------------------------------------------------------------------


    //Dodaje element do kolekcji
    public boolean add(T o){
        AddEnd(o);
        return true;
    }


    //Dodaje wszystkie elementy z kolekcji c do aktualnej listy
    public boolean addAll(Collection c){
        for (Object i : c){
            AddEnd((T) i);
        }
        return true;
    }


    //Usuwa wszystko z obecnej kolekcji
    public void clear() { // czyści wszystko w kolekcji
        elemP = null;
        elemK = null;
    }


    //Sprawdza czy obiekt należy do listy
    public boolean contains(Object o){
        Node<T> temp;
        temp = elemP;
        while(elemP != null) {
            if(elemP.equals(o)) {
                elemP = temp;
                return true;}
            N_elem();
        }
        elemP = temp;
        return false;
    }


    //Sprawdza czy wszystkie elementy kolekcji o należą do listy
    public boolean containsAll(Collection o){
        for(Object i: o) {
            if(!contains(i)) return false;
        }
        return true;
    }


    //Sprawdza czy lista jest pusta
    public boolean isEmpty(){ // czy kolekcja jest pusta?
        return IsEmpty();
    }


    //Nie zostało zaimplementowane
    public Iterator iterator(){
        System.out.println("Nie jest zaimplementowane.");
        return null;
    }


    //Usuwa obiekt z listy, jeżeli do niej należy
    public boolean remove(Object o){
        if(elemP == null) { // kolekcja pusta
            System.out.println("Kolekcja jest pusta.");
            return false;
        }
        else { // kolekcja ma przynajmniej jeden element
            Node<T> temp;
            temp = elemP;
            if(elemP.getValue() == (T) o) { // czy pierwszy element jest równy szukanemu?
                elemP = elemP.getNext();
                return true;
            }
            if(elemK.getValue() == (T) o) { // czy ostatni element jest równy szukanemu?
                elemK = elemK.getPrev();
                return true;
            }
            while(elemP != null) {
                if(elemP.getValue() == (T) o) {
                    elemP.getNext().setPrev(elemP.getPrev());
                    elemP.getPrev().setNext(elemP.getNext());
                    elemP = temp;
                    return true;
                }
                N_elem();
            }
            elemP = temp;
            return false;
        }
    }


    //Usuwa wszystkie te elementy, które należą do kolekji o
    public boolean removeAll(Collection o){
        int licznik = 0;
        for(Object i: o) {
            if(remove(o)) licznik++;
        }
        if(licznik == o.size()) return true;
        return false;
    }


    //Usuwa z listy wszystkie te elementy, których nie ma w kolekcji o
    public boolean retainAll(Collection o) {
        boolean kontrol = false;

        if(elemP == null) { // kolekcja pusta
            System.out.println("Kolekcja jest pusta.");
            return kontrol;
        }
        else { // kolekcja ma przynajmniej jeden element
            Node<T> temp;
            temp = elemP;
            if(!o.contains(elemP)) { // czy pierwszy element jest równy szukanemu?
                elemP = elemP.getNext();
                return true;
            }
            if(!o.contains(elemK)) { // czy ostatni element jest równy szukanemu?
                elemK = elemK.getPrev();
                return true;
            }
            while (elemP != null) {
                if (!o.contains(elemP)) {
                    elemP.getNext().setPrev(elemP.getPrev());
                    elemP.getPrev().setNext(elemP.getNext());
                }
                N_elem();
            }
            elemP = temp;
        }
        return kontrol;
    }


    //Zwraca wielkość listy
    public int size(){
        return howMany();
    }
}

