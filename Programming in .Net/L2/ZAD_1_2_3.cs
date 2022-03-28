using System;
using System.Collections.Generic;
using System.Text;

namespace L2
{
    class ZAD_1_2_3
    {
        public static void Run()
        {
            Console.WriteLine("---------------Zad 1.2.3---------------");


            List<int> list = new List<int>();

            for (int i = 0; i < 16; i++)
            {
                list.Add(i);
            }

            Console.WriteLine("Talica od przekształceń:");
            foreach (int elem in list)
            {
                Console.WriteLine("\t"+elem);
            }

            Console.WriteLine("ConvertAll konwertuje wszystkie elementy tablicy według podanego warunku:");
            List<string> sList = list.ConvertAll(elem => elem.ToString() + "X");
            foreach (string elem in sList)
            {
                Console.WriteLine("\t" + elem);
            }

            Console.WriteLine("FindAll znajduje wszystkie elementy tablicy według podanego warunku:");
            List<int> modFiveList = list.FindAll(elem => elem % 5 == 0);
            foreach (int elem in modFiveList)
            {
                Console.WriteLine("\t" + elem);
            }

            Console.WriteLine("ForEach wykonuje kod dla każdego elementu tablicy:");
            sList.ForEach(delegate (String s)
            {
                if (s.Contains("0")) Console.WriteLine("\t" + s);
            });

            Console.WriteLine("RemoveAll usuwa a tablicy wszystkie elementy, które spełniają podany warunek:");
            Console.WriteLine("Usunąłem {0} elementów",  list.RemoveAll(x => modFiveList.Contains(x)));

            Console.WriteLine("Sort sortuje tablicę według podanej funkcji:");
            list.Sort((x, y) => { if (x <= y) return 1;  else return -1; });
            foreach(int elem in list)
            {
                Console.WriteLine("\t" + elem);
            }
            
        }

    }
}
