using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace L3_DOTNET
{
    class ZAD_1_3_6
    {
        public static void Run()
        {
            if (!File.Exists(@"..\..\..\Requests.txt"))
            {
                Console.WriteLine("ERROR: CAN'T FIND FILE Requests.txt IN MY FOLDER");
                return;
            }

            string line;
            List<string> list = new List<string>();

            using (StreamReader file = new StreamReader(@"..\..\..\Requests.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string l = line.Split(' ')[1];
                    list.Add(l);
                }
                file.Close();
            }

            var result = (from x in list
                          group x by x into y
                          orderby y.Count() descending
                          select new { Adres = y.Key, Count = y.Count() }).Take(3);
            
            Console.WriteLine("-------------Zadanie 1.3.6-------------");
            foreach (var a in result)
            {
                Console.WriteLine("{0}: {1}", a.Adres, a.Count);
            }
        }
    }
}
