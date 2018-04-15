package com.company;

public class Samochod extends Pojazd{
    public String mark;

    Samochod(float e, String t, String m) {
        super(e, t);
        mark = m;
    }

    public String toString() {
        return "Samochód ma silnik; "+typeOfEngine+ " o pojemności: "+
                engineCapacity+" jest marki: "+mark;

    }

}
