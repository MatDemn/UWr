using System;
using System.Collections.Generic;
using System.Text;

namespace L2
{
    public class ListHelper
    {
        public static List<TOutput> ConvertAll<T, TOutput>(List<T> list, Converter<T, TOutput> converter)
        {
            List<TOutput> nowa = new List<TOutput>();
            foreach(T elem in list)
            {
                nowa.Add(converter(elem));
            }
            return nowa;
        }
        public static List<T> FindAll<T>(List<T> list, Predicate<T> match)
        {
            List<T> nowa = new List<T>();
            foreach(T elem in list)
            {
                if (match(elem)) nowa.Add(elem);
            }
            return nowa;
        }
        public static void ForEach<T>(List<T> list, Action<T> action)
        {
            foreach(T elem in list)
            {
                action(elem);
            }
        }
        public static int RemoveAll<T>(List<T> list, Predicate<T> match)
        {
            int ile = 0, index = 0;
            while(index<list.Count)
            {
                if(match(list[index]))
                {
                    list.RemoveAt(index);
                    ile++;
                }
                index++;
            }
            return ile;
        }
        public static void Sort<T>(List<T> list, Comparison<T> comparison)
        {
            for(int i = 0; i<list.Count; i++)
            {
                for(int j = 0; j<list.Count-1; j++)
                {
                    if(comparison(list[j], list[j+1]) > 0)
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
    }

    class ZAD_1_2_4
    {
        
        public static void Run()
        {
            Console.WriteLine("---------------Zad 1.2.4---------------");

            List<int> lista = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                lista.Add(i);
            }

            Console.WriteLine("Lista do operacji:");
            foreach (int elem in lista)
            {
                Console.WriteLine("\t" + elem);
            }

            List<string> sList = ListHelper.ConvertAll(lista, x => x.ToString() + "x");
            Console.WriteLine("ConvertAll:");
            foreach (string elem in sList)
            {
                Console.WriteLine("\t" + elem);
            }

            List<int> fivesList = ListHelper.FindAll(lista, x => x%5 == 0);
            Console.WriteLine("FindAll:");
            foreach (int elem in fivesList)
            {
                Console.WriteLine("\t" + elem);
            }

            Console.WriteLine("ForEach:");
            ListHelper.ForEach(lista, x => Console.WriteLine("{0}", x/2));
            
            Console.WriteLine("RemoveAll:");
            Console.WriteLine("Usunięto elementów: {0}", ListHelper.RemoveAll(sList, x => x.Contains("1")));
            foreach (string elem in sList)
            {
                Console.WriteLine("\t" + elem);
            }


            ListHelper.Sort(lista, (a, b) => b - a);
            Console.WriteLine("Sort:");
            foreach (int elem in lista)
            {
                Console.WriteLine("\t" + elem);
            }

        }
        
    }
}
