using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace L3_DOTNET
{
    class ZAD_1_3_2
    {
        /// <summary>
        /// Generuję 100 numerów z przedziału <1,10000> do pliku Numbers.txt
        /// </summary>
        static void generateNumbersToFile()
        {
            Random rand = new Random();
            int numb;
            
            for (int i = 0; i < 100; i++)
            {
                numb = rand.Next(1, 10000);

                using(StreamWriter file = File.AppendText(@"..\..\..\Numbers.txt"))
                {
                    file.WriteLine(numb.ToString());
                }
            }
        }
        /// <summary>
        /// Czytam numery z pliku i wyświetlam tylko te większe od 100, malejąco.
        /// </summary>
        static void readNumbers()
        {
            int res = 0;
            string line;
            List<int> ints = new List<int>();

            if (!File.Exists(@"..\..\..\Numbers.txt")) { 
                Console.WriteLine("ERROR: CAN'T FIND FILE Numbers.txt IN MY FOLDER");
                return;
            }

            using (StreamReader file = new StreamReader(@"..\..\..\Numbers.txt"))
            {
                while((line = file.ReadLine()) != null)
                {
                    int.TryParse(line, out res);
                    ints.Add(res);
                }
                file.Close();
            }

            var nums = from num in ints
                        where num > 100
                        orderby num descending
                        select num;

            var nums2 = ints.Where(num => num > 100).OrderByDescending(num => num);

            Console.WriteLine("-------------Zadanie 1.3.2-------------");
            Console.WriteLine("Sposób 1 - Wyrażenie Linq");
            foreach (int l in nums) Console.Write("{0} ", l);
            Console.WriteLine("\nSposób 1 - Ciąg wywołań metod Linq");
            foreach (int l in nums2) Console.Write("{0} ", l);
            Console.WriteLine();
        }

        public static void Run()
        {
            //generateNumbersToFile();      // Metoda generująca 100 liczb do pliku Numbers.txt
            readNumbers();
        }
    }
}
