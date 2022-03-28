using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet_1
{
    class Grid
    {
        private int X;
        private int Y;
        private int[][] list;

        /// <summary>
        /// Indekser dwuwymiarowy klasy.
        /// </summary>
        /// <param name="x">Wysokość</param>
        /// <param name="y">Szerokość</param>
        /// <returns>Zwraca element [x][y]</returns>
        public int this[int x, int y]
        {
            get
            {
                return list[x][y];
            }
            set
            {
                list[x][y] = value;
            }
        }
        /// <summary>
        /// Indekser jednowymiarowy klasy.
        /// </summary>
        /// <param name="x">Tablica, któa ma zostać zwrócona.</param>
        /// <returns>Zwraca tablicę o indeksie x.</returns>
        public int[] this[int x]
        {
            get
            {
                return list[x];
            }
        }
        /// <summary>
        /// Konstruktor klasy Grid.
        /// </summary>
        /// <param name="x">Wysokość grida</param>
        /// <param name="y">Szerokość grida</param>
        public Grid(int x, int y)
        {
            X = x;
            Y = y;
            list = new int[x][];
            for (int i = 0; i<x; i++)
            {
                list[i] = new int[y];
            }
        }
        /// <summary>
        /// Wyświetla elementy tablicy.
        /// </summary>
        /// <param name="x">Tablica, której elementy mają zostać wyświetlone.</param>
        public static void printArray(int[] x)
        {
            for(int i = 0; i<x.Length; i++)
            {
                Console.Write(x[i] + "\t");
            }
            Console.WriteLine();
        }
    }
}
