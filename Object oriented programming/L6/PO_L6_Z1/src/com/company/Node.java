package com.company;

import java.io.Serializable;

public class Node<T>  implements Serializable {
    private T value;
    private Node<T> next;
    private Node<T> prev;

    public Node() // konstruktor node, elementu listy
    {
        next = null;
        prev = null;
        value = null;
    }

    public Node(T val) // konstruktor node, elementu listy
    {
        next = null;
        prev = null;
        value = val;
    }

    public T getValue() { return value; }
    public Node<T> getNext() { return next; }
    public Node<T> getPrev() { return prev; }

    public void setValue(T val) { value = val; }
    public void setNext(Node<T> val) { next = val; }
    public void setPrev(Node<T> val) { prev = val; }
}
