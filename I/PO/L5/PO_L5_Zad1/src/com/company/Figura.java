// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L5, z1, Kolekcja figur
// PO_L5_Zad1
// Figura.java
// 2018-04-01

package com.company;

public abstract class Figura implements Comparable<Figura> {
    //Pola klasy
    private float _wysokosc;
    private float _bok1;
    private float _bok2;
    private float _promien;


    //Mutatory oraz akcesory
    public float getWysokosc() {return _wysokosc;}
    public float getBok1() {return _bok1;}
    public float getBok2() {return _bok2;}
    public float getPromien() {return _promien;}

    public void setWysokosc(float val) {_wysokosc = val;}
    public void setBok1(float val) {_bok1 = val;}
    public void setBok2(float val) {_bok2 = val;}
    public void setPromien(float val) {_promien = val;}


    //Konstruktor domyślny figury.
    //Służy jako schemat konstruktora dla wszystkich figur.
    //Zeruje wszystkie pola klasy.
    public Figura() {
        setWysokosc(0);
        setBok1(0);
        setBok2(0);
        setPromien(0);
    }


    //Konstruktor kopiujący pola obiektu Figura do nowego obiektu Figura
    public Figura(Figura temp) {
        setWysokosc(temp.getWysokosc());
        setBok1(temp.getBok1());
        setBok2(temp.getBok2());
        setPromien(temp.getPromien());
    }


    //Zwraca wartość pola figury. Zależnie od klasy obiektu,
    // wylicza pole w różny sposób
    // (pole trójkąta jest liczone inaczej niż pole kwadratu).
    public double getPole() {
        if(this instanceof Prostokat) {
            return getBok1() * getBok2();
        }
        else if(this instanceof Kolo) {
            return Math.PI * getPromien() * getPromien();
        }
        else if(this instanceof Trojkat) {
            return getBok1()*getWysokosc()*0.5;
        }
        else if(this instanceof Trapez) {
            return (getBok1()+getBok2())*getWysokosc()/2.0;
        }
        else return -1;
    }


    //Metoda niezbędna do implementacji interfejsu Comparable.
    //Dzięki niej możemy porównywać ze sobą dwie figury za podstawie ich pól.
    public int compareTo(Figura temp) {
        int dec = Double.compare(getPole(), temp.getPole());
        if (dec == -1) return -1;
        else if (dec == 1) return 1;
        else return 0;
    }


    //Definiuje sposób w jaki będą wyświetlane obiekty klasy,
    //kiedy zostaną wywołane w np. System.out.println();
    public String toString() {
        if(this instanceof Prostokat) {
            return "Kwadrat ma boki: " + getBok1() + " oraz " + getBok2();
        }
        else if(this instanceof Kolo) {
            return "Koło ma promień: " + getPromien();
        }
        else if(this instanceof Trojkat) {
            return "Trójkąt ma podstawę: " + getBok1() +
                    " oraz wysokość: " + getWysokosc();
        }
        else if(this instanceof Trapez) {
            return "Trapez ma podstawę dolną: " + getBok1() +
                    " podstawę górną: " + getBok2() +
                    " oraz wysokość: " + getWysokosc();
        }
        else return "Figura ma parametry: Wysokość: " + getWysokosc()+
                    ", bok1: " + getBok1() +
                    ", bok2: "  + getBok2()+
                    ", promień: "  + getPromien();
    }
}
