using System;

namespace DotNet_4
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime start;
            DateTime end;
            int tests = 1000000;
            int temp;

            // -----------------Argumenty z podanym explicite typem--------------------

            start = DateTime.Now;

            for(int i = 0; i<tests; i++)
            {
                temp = Utils.Make1(i, i);
            }

            end = DateTime.Now;

            Console.WriteLine($"Wyliczony czas przy explicite argumentach: {end - start}");

            // -----------------Argumenty z podanym dynamicznie typem--------------------

            start = DateTime.Now;

            for (int i = 0; i < tests; i++)
            {
                temp = Utils.Make2(i, i);
            }

            end = DateTime.Now;

            Console.WriteLine($"Wyliczony czas przy dynamicznych argumentach: {end - start}");


            // ----------------- Zadanie 2----------------------------------------------

            Utils.saveInWordDocument();

        }
    }
}
