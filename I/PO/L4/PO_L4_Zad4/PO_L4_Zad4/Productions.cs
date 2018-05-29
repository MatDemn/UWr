// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L4, z4, Gramatyki Bezkontekstowe
// PO_L4_Zad4
// Productions.cs
// 2018-03-23

using System;

namespace PO_L4_Zad4
{
    public class Productions
    {
        private string value;
        private Productions prev;
        private Productions next;
        
        /// <summary>
        /// Konstruktor elementu produkcji, tworzy nowy obiekt startowy
        /// </summary>
        public Productions()
        {
            value = "S";
            prev = null;
            next = null;
        }
        
        /// <summary>
        /// Dodaje element przed symbolem startowym
        /// </summary>
        /// <param name="to_add"></param>
        public void addPrev(Productions to_add)
        {
            if (this.prev == null)
            {
                to_add.next = this;
                this.prev = to_add;
            }
            else
            {
                to_add.next = this;
                to_add.prev = this.prev;
                this.prev.next = to_add;
                this.prev = to_add;
            }
        }

        /// <summary>
        /// Dodaje element po symbolu startowym
        /// </summary>
        /// <param name="to_add"></param>
        public void addNext(Productions to_add)
        {
            if (this.next == null)
            {
                to_add.prev = this;
                this.next = to_add;
            }
            else
            {
                to_add.next = this.next;
                to_add.prev = this;
                this.next.prev = to_add;
                this.next = to_add;
            }
        }

        /// <summary>
        /// Właściwość "Value" obiektu
        /// </summary>
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
        
        /// <summary>
        /// Metoda wypisująca na ekranie całe utworzone dotychczas słowo
        /// </summary>
        public void Wypisz()
        {
            Productions temp = new Productions();
            temp = this;
            while (temp.prev != null) temp = temp.prev;
            while (temp != null)
            {
                if(temp.Value != "*")
                    Console.Write(temp.Value);
                temp = temp.next;
            }
            Console.WriteLine("");
            Console.WriteLine("-------------------");
        }
        
        
        /// <summary>
        /// Metoda do testów,
        /// wyświetla oprócz wszystkich elementów utworzonego słowa
        /// także elementy ze słowem pustym (tutaj wyświetlanym jako '*')
        /// </summary>
        public void DevWypisz()
        {
            Productions temp = new Productions();
            temp = this;
            while (temp.prev != null) temp = temp.prev;
            while (temp != null)
            {
                Console.Write(temp.Value);
                temp = temp.next;
            }
            Console.WriteLine("");
            Console.WriteLine("-------------------");
        }
    }
}