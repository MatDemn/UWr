package com.company;

import java.io.Serializable;

abstract class Pojazd implements Serializable {
    public double engineCapacity;
    public String typeOfEngine;

    Pojazd(double e, String t) {
        engineCapacity = e;
        typeOfEngine = t;
    }
}
