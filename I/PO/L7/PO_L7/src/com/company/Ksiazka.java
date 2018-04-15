package com.company;

import java.io.Serializable;

abstract class Ksiazka implements Serializable {
    public String title;
    public String print;

    Ksiazka(String t, String p) {
        title = t;
        print = p;
    }
}
