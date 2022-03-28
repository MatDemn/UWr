// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L4, z2, PrimeCollection
// PO_L4_Zad2
// PrimeCollection.cs
// 2018-03-23

using System.Collections;

namespace PO_L4_Zad2
{
    /// <summary>
    /// Klasa pomocnicza, do wyliczania kolejnych liczb pierwszych
    /// </summary>
    public class Prime_Num : IEnumerator
    {
        
        /// <summary>
        /// Pola klasy
        /// </summary>
        private int n;
        
        
        /// <summary>
        /// Konstruktor obiektu klasy PrimeCollection
        /// </summary>
        public Prime_Num() {
            n = -1;
        }

        
        /// <summary>
        /// Sprawdenie czy liczba int n jest pierwsza
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool is_Prime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }
        

        /// <summary>
        /// Metoda current do interfejsu IEnumerator
        /// </summary>
        public object Current
        {
            get { return n; }
        }

        
        /// <summary>
        /// Metoda movenext do interfejsu IEnumerator
        /// </summary>
        bool IEnumerator.MoveNext()
        {
            n++;
            while (!is_Prime(n) && n!= int.MaxValue) n++;
            return n < int.MaxValue;
        }
        

        /// <summary>
        /// Metoda reset do interfejsu IEnumerator
        /// </summary>
        void IEnumerator.Reset()
        {
            n = -1;
        }
 
    }
    class PrimeCollection : IEnumerable
    {
        /// <summary>
        /// GetEnumerator do interfejsu IEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return new Prime_Num();
        }
    }
}