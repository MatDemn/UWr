package com.company;

import java.io.Serializable;

abstract class Figura implements Serializable {
    public String colour;
    public String name;

    Figura() {
        colour = "";
        name = "";
    }

    Figura(String c, String n) {
        colour = c;
        name = n;
    }

    abstract public void guiFig(String s);

    abstract public void saveObject(String s);

    abstract public void loadObject(String s);
}
