using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace L2
{
    class Kwadrat
    {
        public int example { set; get; }
        public int Width { set; get; }
        public int Height { set; get; }
        
        private int Pole1 { set; get; }
        private int Pole2 { set; get; }
        
        private int CalculateArea()
        {
            return Height * Width;
        }

        [Oznakowane]
        public int Bar1()
        {
            Console.WriteLine("Wywołano metodę Bar1, OZNAKOWANE");
            return 42;
        }

        public int Foo()
        {
            Console.WriteLine("Wywołano metodę Foo, NIEOZNAKOWANE");
            return 52;
        }

        public int Foo1()
        {
            Console.WriteLine("Wywołano metodę Foo1, NIEOZNAKOWANE");
            return 52;
        }

        [Oznakowane]
        public int Bar()
        {
            Console.WriteLine("Wywołano metodę Bar, OZNAKOWANE");
            return 42;
        }

        public Kwadrat(int h, int w)
        {
            Height = h;
            Width = w;
            Pole1 = h;
            Pole2 = w;
            example = 42;
        }
    }
}
