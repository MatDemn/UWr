using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet_1
{
    class ZAD1
    {
        /// <summary>
        /// Rozbija inta na listę intów (lista cyfr inta).
        /// </summary>
        /// <param name="x">Liczba, której cyfry mają zostać włożone do tablicy.</param>
        /// <returns>Zwraca listę cyfr liczby.</returns>
        private static List<int> splitIntoDigits_arith(int x)
        {
            List<int> result = new List<int>();
            while (x != 0)
            {
                result.Add(x % 10);
                x /= 10;
            }
            return result;
        }
        /// <summary>
        /// Czy int jest podzielny przez jego cyfry.
        /// </summary>
        /// <param name="x">Liczba do sprawdzenia.</param>
        /// <returns>True/False</returns>
        public static bool divisibleByDigits(int x)
        {
            List<int> xlist = splitIntoDigits_arith(x);
            for (int i = 0; i < xlist.Count; i++)
            {
                if (xlist[i] != 0 && x % xlist[i] != 0) return false;
            }
            return true;
        }
        /// <summary>
        /// Czy int jest podzielny przez sumę jego cyfr.
        /// </summary>
        /// <param name="x">Liczba do sprawdzenia.</param>
        /// <returns>True/False</returns>
        public static bool divisibleBySumOfDigits(int x)
        {
            List<int> xlist = splitIntoDigits_arith(x);
            int sum = 0;
            for (int i = 0; i < xlist.Count; i++)
            {
                sum += xlist[i];
            }
            return x % sum == 0;
        }
    }
}
