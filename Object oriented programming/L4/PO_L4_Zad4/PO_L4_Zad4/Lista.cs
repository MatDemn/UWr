// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L4, z4, Gramatyki Bezkontekstowe
// PO_L4_Zad4
// Lista.cs
// 2018-03-23

using System;

namespace PO_L4_Zad4
{
    public class Lista
    {
        private Productions[] listOfProductions;
        private int lastAdded;
        private int indexer;
        private int listSize;

        
        /// <summary>
        /// Dodaje referencje do symboli startowych
        /// do tablicy listOfProductions
        /// </summary>
        /// <param name="elem"></param>
        private void addToList(Productions elem)
        {
            if (LastAdded == ListSize-1) Console.WriteLine("Lista pełna.");
            else
            {
                LastAdded++;
                listOfProductions[LastAdded] = elem;
            }
        }
        

        /// <summary>
        /// Wypisuje całe słowo
        /// </summary>
        public void Write()
        {
            listOfProductions[0].Wypisz();
        }
        
        
        /// <summary>
        /// Wypisuje całe słowo, łącznie z oznaczeniem słów pustych,
        /// tutaj jako "*"
        /// </summary>
        public void DevWrite()
        {
            listOfProductions[0].DevWypisz();
        }
        
        
        /// <summary>
        /// Właściwość LastAdded
        /// </summary>
        private int LastAdded
        {
            get { return lastAdded; }
            set { lastAdded = value; }
        }
        

        /// <summary>
        /// Właściwość Indexer
        /// </summary>
        private int Indexer
        {
            get { return indexer;}
            set { indexer = value; }
        }
        

        /// <summary>
        /// Właściwość ListSize
        /// </summary>
        private int ListSize
        {
            get { return listSize; }
            set { listSize = value; }
        }
        

        /// <summary>
        /// Metoda odpowiadająca produkcji: S -> aSb
        /// </summary>
        public void Prod_ab()
        {
            if (listOfProductions[Indexer].Value == "S")
            {
                Productions n1 = new Productions();
                n1.Value = "a";
                Productions n2 = new Productions();
                n2.Value = "b";

                listOfProductions[Indexer].addPrev(n1);
                listOfProductions[Indexer].addNext(n2);
            }
        }

        
        /// <summary>
        /// Metoda odpowiadająca produkcji: S -> cSd
        /// </summary>
        public void Prod_cd()
        {
            if (listOfProductions[Indexer].Value == "S")
            {
                Productions n1 = new Productions();
                n1.Value = "c";
                Productions n2 = new Productions();
                n2.Value = "d";
    
                listOfProductions[Indexer].addPrev(n1);
                listOfProductions[Indexer].addNext(n2);
            }
        }
        

        /// <summary>
        /// Metoda dopowiadająca produkcji: S -> * (słowo puste epsilon)
        /// </summary>
        public void Prod_empty()
        {
            listOfProductions[Indexer].Value = "*";
            //Console.WriteLine("#-#-#-#-#");
            //Write();
            //Console.WriteLine("#-#-#-#-#");
            //Console.ReadKey();

            if(Indexer != LastAdded) 
                Indexer++;
        }
        

        /// <summary>
        /// Metoda dopowiadająca produkcji: S -> SS
        /// </summary>
        public void Prod_double()
        {
            if (listOfProductions[Indexer].Value == "S")
            {
                Productions nowy = new Productions();
                if (LastAdded != ListSize - 1)
                {
                    listOfProductions[Indexer].addPrev(nowy);
                    addToList(nowy);
                }
            }
        }
        

        /// <summary>
        /// Konstruktor listy, z zaimplementowanym tworzeniem losowych słów
        /// </summary>
        /// <param name="maxSizeConst"></param>
        public Lista(int maxSizeConst)
        {
            Random rand = new Random(); //tworzę obiekt losujący wartości
            int size = rand.Next(10, maxSizeConst); //losuję wielkość tablicy
            listOfProductions = new Productions[size]; //tworzę tablicę 
            // o wylosowanej wielkości
            LastAdded = -1; //ustawiam indeks ostatnio dodanego elementu na -1
            Indexer = 0; //rozpoczynam indeksowanie w talicy od 0
            listSize = size; //ustalam wielkość listy na wylosowaną wcześniej
            addToList(new Productions()); //dodaję do listy symboli startowych
            //jeden element, żeby móć od czegoś zacząć
            while (listOfProductions[Indexer].Value == "S") // dopóki mamy 
                //element startowy
            {
                int decyzja = rand.Next(10); //losuję produkcję
                if(decyzja < 4) Prod_ab();
                else if(decyzja < 7) Prod_cd();
                else if(decyzja == 7) Prod_double();
                else if(decyzja < 10) Prod_empty();
            }
        }
    }
}