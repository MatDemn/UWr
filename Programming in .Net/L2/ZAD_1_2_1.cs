using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace L2
{
    class ZAD_1_2_1
    {
        public static void Run()
        {
            Console.WriteLine("---------------Zad 1.2.1---------------");
            // Tworzę obiekty klas ArrayList oraz List

            List<int> lista = new List<int>();
            ArrayList Alista = new ArrayList();
            int liczbaProb = 10000;

            DateTime start;
            DateTime end;
            TimeSpan czas;

            //Dodawanie elementów
            Console.WriteLine("Dodawanie elementów dla List");
            start = DateTime.Now;
            for(int i = 0; i<liczbaProb; i++)
            {
                lista.Add(i);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            Console.WriteLine("Dodawanie elementów dla ArrayList");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                Alista.Add(i);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            // Przeglądanie elementów

            Console.WriteLine("Przeglądanie elementów dla List");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                lista.Contains(liczbaProb / 2);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            Console.WriteLine("Przeglądanie elementów dla ArrayList");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                Alista.Contains(liczbaProb / 2);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            // Usuwanie elementow

            Console.WriteLine("Usuwanie elementów dla List");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                lista.Remove(lista.Count - 1);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            Console.WriteLine("Usuwanie elementów dla ArrayList");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                lista.Remove(lista.Count - 1);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);


            // Tworzę obiekty klas HashTable oraz Dictionary
            Dictionary<int, int> dict = new Dictionary<int, int>();
            Hashtable htab = new Hashtable();


            //Dodawanie elementów
            Console.WriteLine("Dodawanie elementów dla Dictionary");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                dict.Add(i, i);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            Console.WriteLine("Dodawanie elementów dla Hashtable");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                htab.Add(i, i);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            // Przeglądanie elementów

            Console.WriteLine("Przeglądanie elementów dla Dictionary");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                dict.ContainsValue(liczbaProb / 2);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            Console.WriteLine("Przeglądanie elementów dla Hashtable");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                htab.Contains(liczbaProb / 2);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            // Usuwanie elementow

            Console.WriteLine("Usuwanie elementów dla Dictionary");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                dict.Remove(lista.Count - 1);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);

            Console.WriteLine("Usuwanie elementów dla Hashtable");
            start = DateTime.Now;
            for (int i = 0; i < liczbaProb; i++)
            {
                htab.Remove(lista.Count - 1);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("\tCzas: {0}\n", czas);
        }
    }
}
