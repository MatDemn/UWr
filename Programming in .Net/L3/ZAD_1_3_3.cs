using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace L3_DOTNET
{
    class ZAD_1_3_3
    {
        public static void Run()
        {
            if (!File.Exists(@"..\..\..\Names.txt"))
            {
                Console.WriteLine("ERROR: CAN'T FIND FILE Names.txt IN MY FOLDER");
                return;
            }

            string line;
            List<string> names = new List<string>();

            using (StreamReader file = new StreamReader(@"..\..\..\Names.txt"))
            {
                while((line = file.ReadLine()) != null)
                {
                    names.Add(line);
                }
                file.Close();
            }

            var namesLinq = from n in names
                            group n by n[0] into a
                            orderby a.Key
                            select a.Key;

            Console.WriteLine("-------------Zadanie 1.3.3-------------");
            Console.WriteLine("Lista pierwszych liter nazwisk:");
            foreach (char l in namesLinq) Console.Write("{0} ", l);
            Console.WriteLine();

        }
    }
}
