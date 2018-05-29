using System.Globalization;
using System;

namespace PO_L4_Zad2
{
    public class Lista<T>
    {
        private Node<T> elemP;
        private Node<T> elemK;


        public Lista() // konstruktor listy
        {
            elemP = null;
            elemK = null;
        }

        public void AddBegin(T val)    // dodanie na początek listy
        {
            if (elemP == null) // kiedy lista jest pusta
            {
                
                elemP = new Node<T>();
                elemP.Value = val;
                elemK = elemP;
            }
            else if (elemP == elemK) // kiedy jest dokładnie jeden element
            {
                Node<T> nowy = new Node<T>(); // tworzę nowy pojemnik
                nowy.Value = val; // nadaję mu wartość
                elemP = nowy; // jest jeden element, 
                //wiec chcę ten pierwszy element traktować jako nowy
                elemP.Next = elemK; // przyczepiam P do glowy K
                elemK.Prev = elemP; // robię referencję 
                //do poprzedniego elementu, czyli P
            }
            else // kiedy jest więcej niż jeden element
            {
                Node<T> nowy = new Node<T>(); // tworze nowy pojemnik
                Node<T> temp = new Node<T>(); // tworze temp
                nowy.Value = val; // nadaje mu wartosc
                temp = elemP; // zapisuje referencje do glowy aktualnej
                elemP = nowy; // podmieniam poczatek z nowym
                elemP.Next = temp; // podpinam 
                temp.Prev = elemP;
            }

            Console.WriteLine("#Metoda: Wykonałem AddBegin!");
        }

        public Node<T> DelBegin() // usunięcie z początku listy
        {
            if (elemP == null) // lista jest pusta
            {
                Console.WriteLine("#Metoda: Niestety, lista jest pusta!");
                return elemP;
            }
            else if (elemP == elemK) // na liście jest dokładnie jeden element

            {
                Node<T>temp = new Node<T>();
                temp = elemP;
                elemP = elemP.Next;
                elemK = elemK.Next;
                Console.WriteLine("#Metoda: Wykonałem DelBegin!");
                return temp;
            }
            else // na liście jest więcej niż jeden element
            {
                Node<T>temp = new Node<T>();
                temp = elemP;
                elemP = elemP.Next;
                Console.WriteLine("#Metoda: Wykonałem DelBegin!");
                return temp;
            }
        }

        public void AddEnd(T val) // dodanie na koniec listy
        {
            if (elemK == null) // lista jest pusta
            {
                
                elemK = new Node<T>();
                elemK.Value = val;
                elemP = elemK;
            }
            else if (elemP == elemK) // lista ma jeden element
            {
                Node<T> nowy = new Node<T>(); 
                nowy.Value = val; 
                elemK = nowy; 
                elemK.Prev = elemP; 
                elemP.Next = elemK; 
            }
            else // lista ma więcej niż jeden element
            {
                Node<T> nowy = new Node<T>(); 
                Node<T> temp = new Node<T>(); 
                nowy.Value = val; 
                temp = elemK; 
                elemK = nowy; 
                elemK.Prev = temp; 
                temp.Next = elemK;
            }

            Console.WriteLine("#Metoda: Wykonałem AddBegin!");
        }

        public Node<T> DelEnd() // usunięcie z końca listy
        {
            if (elemK == null) // lista jest pusta
            {
                Console.WriteLine("#Metoda:Niestety, lista jest pusta!");
                Console.WriteLine("#Metoda:Wykonałem DelEnd!");
                return elemK;
            }
            else if (elemP == elemK) // lista ma dokładnie jeden element

            {
                Node<T> temp = new Node<T>();
                temp = elemK;
                elemK = elemK.Prev;
                elemP = elemP.Prev;
                Console.WriteLine("#Metoda:Wykonałem DelEnd!");
                return temp;
            }
            else // lista ma więcej niż jeden element
            {
                Node<T> temp = new Node<T>();
                temp = elemK;
                elemK = elemK.Prev;
                elemK.Next = null;
                Console.WriteLine("#Metoda: Wykonałem DelEnd!");
                return temp;
            }
        }

        public bool IsEmpty() // sprawdza czy lista jest pusta
        {
            if (elemP == null && elemK == null) return true;
            return false;
        }

        public T CheckP() // zwraca element z początku listy
        {
            if (elemP == null)
            {
                Console.WriteLine(" #Metoda: UPS! Nie ma elementu! " +
                                  "Dam Ci po prostu default.");
                return default(T);
            }
            return elemP.Value;
        }
        
        public T CheckK() // zwraca element z końca listy
        {
            if (elemK == null)
            {
                Console.WriteLine(" #Metoda: UPS! Nie ma elementu! " +
                                  "Dam Ci po prostu default.");
                return default(T);
            }
            return elemK.Value;
        }

        public void N_elem() // przechodzi do kolejnego elementu
        {
            if(elemP.Next == null) Console.WriteLine("#Metoda: KONIEC LISTY");
            elemP = elemP.Next;
        }
        
        public void P_elem() // przechodzi do poprzedniego elementu
        {
            if(elemP.Prev == null) Console.WriteLine("#Metoda: KONIEC LISTY");
            elemP = elemP.Prev;
        }


        public Node<T> ElemP // zwraca obiekt z początku listy, 'elemP'
        {
            get { return elemP; }
            set { this.elemP = value; }
        }

    }
}
