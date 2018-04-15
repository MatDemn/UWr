package com.company;

public class Tramwaj extends Pojazd {
    public int carCapacity;

    Tramwaj(float e, String t, int c) {
        super(e, t);
        carCapacity = c;
    }

    public String toString() {
        return "Tramwaj ma silnik; "+typeOfEngine+ " o pojemności: "+
                engineCapacity+" może pomieścić: "+carCapacity+" pasażerów.";

    }
}
