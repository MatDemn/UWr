package com.company;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.io.*;

public class Okrag extends Figura {
    public double radius;


    //Konstruktor z argumentami.
    Okrag(String c, String n, double r) {
        super(c, n);
        radius = r;
    }


    //Metoda, która definiuje sposób wyświetlania obiektu w konsoli.
    public String toString() {
        return "Parametry okręgu: nazwa: "+name+" kolor: "+
                colour+" promień: "+radius;
    }


    //Metoda zapisująca do pliku nowe dane obiektu.
    public void saveObject(String s) {
        try (ObjectOutputStream outputStream = new ObjectOutputStream(new FileOutputStream(s+".bin"))) {
            outputStream.writeObject(this);
        }
        catch(IOException e) {
            System.out.println("IOException occured.");
        }
    }


    //Metoda czytająca z pliku dane obiektu.
    public void loadObject(String s) {
        try (ObjectInputStream input = new ObjectInputStream(new FileInputStream(s+".bin"))) {
            Okrag readObject = (Okrag) input.readObject();
            colour = readObject.colour;
            name = readObject.name;
            radius = readObject.radius;
            System.out.println(this);
        }
        catch(FileNotFoundException e) {
            System.out.println("Nie znaleziono pliku. " +
                    "Tworzę nowy z domyślnymi wartościami.");
        }
        catch(IOException e) {
            System.out.println("Wystąpił wyjątek.");
        }
        catch(ClassNotFoundException e) {
            System.out.println("Nie znaleziono klasy.");
        }
    }


    //Metoda wyświetlająca interfejs do edycji pól obiektu.
    public void guiFig(String s) {
        loadObject(s);

        JFrame frame = new JFrame("Edycja okręgu");
        JPanel mainPanel = new JPanel();

        JTextField Tname = new JTextField(name,25);
        JTextField Tcolour = new JTextField(colour,25);
        JTextField Tradius = new JTextField(Double.toString(radius),25);

        JButton but1 = new JButton("Zapisz");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(300, 300);
        frame.add(mainPanel);

        mainPanel.add(new JLabel("Nazwa: "));
        mainPanel.add(Tname);
        mainPanel.add(new JLabel("Kolor: "));
        mainPanel.add(Tcolour);
        mainPanel.add(new JLabel("Promień: "));
        mainPanel.add(Tradius);
        mainPanel.add(but1);
        frame.setVisible(true);


        //Utworzenie "słuchacza".
        // Dzięki tej części kodu przycisk otrzymuje
        // możliwość zapisu wprowadzonych danych.
        but1.addActionListener(
                new ActionListener() {
                    public void actionPerformed(ActionEvent event) {
                        if(event.getSource()==but1) {
                            name = Tname.getText();
                            colour = Tcolour.getText();
                            radius = Double.parseDouble(Tradius.getText());
                            saveObject(s);
                            JOptionPane.showMessageDialog(frame,
                                    "Object saved.");
                            JOptionPane.showMessageDialog(frame,
                                    "Nazwa: "+name+
                                            " kolor: "+colour+
                                            " promień: "+radius);
                        }
                    }
                }
        );

    }
}
