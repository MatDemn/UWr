// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L4, z2, PrimeCollection
// PO_L4_Zad2
// Program.cs
// 2018-03-23

using System;
using System.Diagnostics.Eventing.Reader;

namespace PO_L4_Zad2
{
    internal class Program
    {
        public static void Main(string[] args)
        {  
            Console.WriteLine("    >>Tworzę obiekt klasy PrimeCollection");
            PrimeCollection pc = new PrimeCollection();
            Console.WriteLine("    >>Przechodzę po wszystkich elementach");
            System.Threading.Thread.Sleep(3000); // program czeka 3 sekundy, 
            //aby można było w konsoli przeczytać dwa powyższe komunikaty.
            foreach(int p in pc)
                Console.WriteLine(p);
            Console.WriteLine("    >>KONIEC PROGRAMU");
        }
    }
}