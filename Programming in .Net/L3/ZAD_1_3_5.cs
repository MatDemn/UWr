using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace L3_DOTNET
{
    class ZAD_1_3_5
    {
        public static void Run()
        {
            if (!File.Exists(@"..\..\..\Klient.txt"))
            {
                Console.WriteLine("ERROR: CAN'T FIND FILE Klient.txt IN MY FOLDER");
                return;
            }

            if (!File.Exists(@"..\..\..\Konto.txt"))
            {
                Console.WriteLine("ERROR: CAN'T FIND FILE Konto.txt IN MY FOLDER");
                return;
            }

            string line;
            List<Klient> klientList = new List<Klient>();
            List<Konto> kontoList = new List<Konto>();

            using (StreamReader file = new StreamReader(@"..\..\..\Klient.txt"))
            {
                while((line = file.ReadLine()) != null)
                {
                    string[] l = line.Split(',');
                    klientList.Add(new Klient(l[0], l[1], l[2]));
                }
                file.Close();
            }

            using (StreamReader file = new StreamReader(@"..\..\..\Konto.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] l = line.Split(',');
                    kontoList.Add(new Konto(l[0], l[1]));
                }
                file.Close();
            }

            var lista = from x in klientList
                             join y in kontoList
                             on x.PESEL equals y.PESEL
                             select new
                             {
                                 x.Imie,
                                 x.Nazwisko,
                                 x.PESEL,
                                 y.NumerKonta
                             };
            Console.WriteLine("-------------Zadanie 1.3.5-------------");
            foreach (var a in lista)
            {
                Console.WriteLine("{0} {1} \t{2}", a.Imie, a.Nazwisko, a.PESEL);
                Console.WriteLine("Konto: \t{0}", a.NumerKonta);
            }
        }
    }
}
