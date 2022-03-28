using System;
using System.Collections.Generic;
using System.Text;

namespace L3_DOTNET
{
    class Klient
    {
        public string Imie{ get; set; }
        public string Nazwisko { get; set; }
        public string PESEL { get; set; }

        public Klient(string i, string n, string p)
        {
            Imie = i;
            Nazwisko = n;
            PESEL = p;
        }
    }
}
