package com.company;

public class Czasopismo extends Ksiazka {
    public int pages; //ile ma stron

    Czasopismo(String t, String p, int pa) {
        super(t, p);
        pages = pa;
    }

    public String toString() {
        return "Czasopismo "+title
                +" drukowane przez "+print+", ma stron "+pages;
    }
}
