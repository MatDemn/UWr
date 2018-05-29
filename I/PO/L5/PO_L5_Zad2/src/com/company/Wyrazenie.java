// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z2, Wyrażenia arytmetyczne
// PO_L5_Zad2
// Wyrazenie.java
// 2018-04-01

package com.company;
//import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import java.util.LinkedHashMap;
//import java.util.Set;
//import java.util.TreeMap;
import java.util.HashMap;

public abstract class Wyrazenie {


    //Pola klasy
    private Wyrazenie _leftside;
    private Wyrazenie _rightside;


    //Akcesory i mutatory
    public Wyrazenie getLeft() {return _leftside;}
    public Wyrazenie getRight() {return _rightside;}

    public void setLeft(Wyrazenie val) {_leftside = val;}
    public void setRight(Wyrazenie val) {_rightside = val;}


    // Konstruktor tylko dla klasy "Stala"
    // z racji tego, że stala nie używa prawej i lewej strony.
    // Użycie innego konstruktora poskutkowałoby nieskończoną rekurencją
    // (ponieważ pola _leftside oraz _rightside są typu Wyrazenie).
    public Wyrazenie() {
        _leftside = null;
        _rightside = null;
    }


    //Konstruktor z parametrami. Tworzy nowe wyrażenie za pomocą składowych
    //_leftside i _rightside.
    public Wyrazenie(Wyrazenie a, Wyrazenie b) {
        _leftside = a;
        _rightside = b;
    }


    //Domyślna metoda wyliczająca wartość.
    // Musi być obecna, aby podklasy mogły ją nadpisać.
    //Nie powinna być nigdy wywołana,
    // ponieważ nie można obliczyć niezdefiniowanego wyrażenia.
    public int oblicz() {
        System.out.println("For formal purposes only.");
        System.out.println("Undefined expression.");
        return 404;
    }
}
