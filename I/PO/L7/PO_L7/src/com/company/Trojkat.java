package com.company;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.io.*;

public class Trojkat extends Figura implements Serializable  {
    public double height;
    public double base;

    Trojkat(String c, String n, double h, double b) {
        super(c, n);
        height = h;
        base = b;

    }

    public void saveObject(String s) {
        try (ObjectOutputStream outputStream = new ObjectOutputStream(new FileOutputStream(s+".bin"))) {
            outputStream.writeObject(this);
        }
        catch(IOException e) {
            System.out.println("Wystąpił wyjątek.");
        }
    }
    public void loadObject(String s) {
        try (ObjectInputStream input = new ObjectInputStream(new FileInputStream(s+".bin"))) {
            Trojkat readObject = (Trojkat) input.readObject();
            colour = readObject.colour;
            name = readObject.name;
            height = readObject.height;
            base = readObject.base;
            System.out.println(this);
        }
        catch(FileNotFoundException e) {
            System.out.println("Nie znaleziono pliku. " +
                    "Wczytuję wartości domyślne.");
        }
        catch(IOException e) {
            System.out.println("Wystąpił wyjątek.");
        }
        catch(ClassNotFoundException e) {
            System.out.println("Nie znaleziono klasy.");
        }
    }

    public void guiFig(String s) {

        loadObject(s);

        JFrame frame = new JFrame("Edycja trójkąta");
        JPanel mainPanel = new JPanel();

        JTextField Tname = new JTextField(name, 25);
        JTextField Tcolour = new JTextField(colour,25);
        JTextField Tbase = new JTextField(Double.toString(base),25);
        JTextField Theight = new JTextField(Double.toString(height),25);

        JButton but1 = new JButton("Zapisz");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(300, 300);
        frame.add(mainPanel);

        mainPanel.add(new JLabel("Nazwa: "));
        mainPanel.add(Tname);
        mainPanel.add(new JLabel("Kolor: "));
        mainPanel.add(Tcolour);
        mainPanel.add(new JLabel("Podstawa: "));
        mainPanel.add(Tbase);
        mainPanel.add(new JLabel("Wysokość: "));
        mainPanel.add(Theight);
        mainPanel.add(but1);
        frame.setVisible(true);

        but1.addActionListener(
                new ActionListener() {
                    public void actionPerformed(ActionEvent event) {
                        if(event.getSource()==but1) {
                            name = Tname.getText();
                            colour = Tcolour.getText();
                            base = Double.parseDouble(Tbase.getText());
                            height = Double.parseDouble(Theight.getText());
                            saveObject(s);
                            JOptionPane.showMessageDialog(frame,
                                    "Object saved.");
                            JOptionPane.showMessageDialog(frame,
                                    "Nazwa: "+name+
                                            " kolor: "+colour+
                                            " podstawa: "+base+
                                            " wysokość: "+height);
                        }
                    }
                }
        );
    }

    public String toString() {
        return "Parametry trójkąta: nazwa: "+name+" kolor: "+
                colour+" podstawa: "+base+" wysokość: "+height;

    }
}
