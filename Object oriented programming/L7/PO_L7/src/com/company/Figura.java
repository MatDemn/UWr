package com.company;

import java.io.Serializable;

abstract class Figura implements Serializable {
    public String colour;
    public String name;


    //Konstruktor domyślny, bez argumentów.
    Figura() {
        colour = "";
        name = "";
    }


    //Konstruktor z parametrmi.
    Figura(String c, String n) {
        colour = c;
        name = n;
    }


    //Abstrakcyjne metody klasy.
    // Wszystkie podklasy będą posiadały właściwą ich implementację.

    //Wyświetlenie interfejsu do edycji pól figury.
    abstract public void guiFig(String s);


    //Metoda do zapisu do pliku edytowanych danych.
    abstract public void saveObject(String s);


    //Metoda do czytania z pliku danych obiektu.
    abstract public void loadObject(String s);
}
