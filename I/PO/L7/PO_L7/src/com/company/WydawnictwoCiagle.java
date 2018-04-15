package com.company;

public class WydawnictwoCiagle extends Ksiazka {
    public String number; //który to numer/wydanie

    WydawnictwoCiagle(String t, String p, String n) {
        super(t, p);
        number = n;
    }

    public String toString() {
        return "Wydawnictwo ciągłe "+title
                +" drukowane przez "+print+", wydanie "+number;
    }
}
