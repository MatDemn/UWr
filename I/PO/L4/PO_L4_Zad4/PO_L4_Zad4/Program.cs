// Mateusz Zając
// Pracownia PO, piątek, s. 137
// L4, z4, Gramatyki Bezkontekstowe
// PO_L4_Zad4
// Program.cs
// 2018-03-23

using System;

namespace PO_L4_Zad4
{
    internal class Program
    {
        public static void Main(string[] args)
        {   
            const int maxSizeConst = 15; // tyle najwięcej symboli startowych
            //może liczyć słowo
            Lista myProd = new Lista(maxSizeConst);
            Console.WriteLine(">>Wynik wywołania konstruktora " +
                              "losowych słów: ");
            myProd.Write();
            Console.WriteLine(">>Ten sam ciąg, z wyświetlonymi " +
                              "słowami pustymi '*'");
            myProd.DevWrite();
            Console.ReadKey();
        }
    }
}