using System;
using System.Collections.Generic;

namespace DotNet_1
{
    class Start
    {
        /// <summary>
        /// Wywołanie obliczeń dla zadania 1.
        /// </summary>
        /// <param name="end">Ilość elementów do sprawdzenia (>2).</param>
        public static void Run1(int end)
        {
            Console.WriteLine("Zad1");
            for (int i = 1; i <= end; i++)
            {
                if (ZAD1.divisibleByDigits(i) && ZAD1.divisibleBySumOfDigits(i)) Console.WriteLine(i);
            }
        }
        /// <summary>
        /// Główna funkcja programu, startup.
        /// </summary>
        /// <param name="args">Argumenty uruchomienia.</param>
        static void Main(string[] args)
        {
            // ---------------ZAD 1---------------
            Console.WriteLine("ZADANIE 1");
            Run1(100000);
            // ---------------ZAD 2---------------
            Console.WriteLine("ZADANIE 2");
            Grid g = new Grid(4, 4);
            Console.WriteLine("Pobieranie pojedynczych wierszy");
            Grid.printArray(g[0]);
            Grid.printArray(g[1]);
            Grid.printArray(g[2]);
            Grid.printArray(g[3]);
            Console.WriteLine();
            Console.WriteLine("Pobieranie elementów tablicy");
            g[0, 2] = 62;
            g[1, 2] = 42;
            g[3, 2] = 52;
            Grid.printArray(g[0]);
            Grid.printArray(g[1]);
            Grid.printArray(g[2]);
            Grid.printArray(g[3]);
            Console.WriteLine(g[0, 2]);
            Console.WriteLine(g[1, 2]);
            Console.WriteLine(g[3, 2]);


        }
    }
}
