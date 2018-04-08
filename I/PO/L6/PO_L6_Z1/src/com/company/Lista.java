package com.company;

import java.io.Serializable;
import java.util.Collection;

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
    {
        if(elemP.getNext() == null)
            System.out.println("#Metoda: KONIEC LISTY");
        elemP = elemP.getNext();
    }

    public void P_elem() // przechodzi do poprzedniego elementu
    {
        if(elemP.getPrev() == null) System.out.println("#Metoda: KONIEC LISTY");
        elemP = elemP.getPrev();
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

    public boolean removeAll(Collection<?> obj) {
        return true;
    }

    //------------------------------------------------------------------------
    //------------------------------------------------------------------------


    public boolean add(T o){
        AddEnd(o);
        return true;
    }

    public boolean addAll(Collection c){
        for (Object i : c){
            AddEnd((T) i);
        }
        return true;
    }

    public void clear() { // czyści wszystko w kolekcji
        elemK = elemP;
        elemP.setValue(null);
        elemP.setNext(null);
        elemP.setPrev(null);
    }

    public boolean contains(Object o){ // czy kolekcja zawiera element?
        Node<T> temp;
        temp = elemP;
        while(elemP != null) {
            if(elemP == o) {
                elemP = temp;
                return true;}
            N_elem();
        }
        elemP = temp;
        return false;
    }

    public boolean containsAll(Collection o){ // czy kolekcja zawiera
        // wszystkie elementy kolekcji o?
        for(Object i: o) {
            if(!contains(i)) return false;
        }
        return true;
    }

    public boolean isEmpty(){ // czy kolekcja jest pusta?
        return IsEmpty();
    }

    public Iterator iterator(){ //
        return null;

    }

    public boolean remove(Object o){ // usuwa obiekt o z kolekcji
        Node<T> temp;
        temp = elemP;
        while(elemP != null) {

        }
        elemP = temp;
    }

    public boolean removeAll(Collection o){ // przeciwieństwo retain, jeśli element jest w c, usuwa go z this
        return false;
    }

    public boolean retainAll(Collection o){ // ma zachowywać te elementy, któe są w collection o, a usuwać te co ich nie ma
        return false;
    }

    public int size(){ //zwraca wielkość kolekcji
        return 1;
    }

    }
}
