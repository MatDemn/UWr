using System;
using System.Collections.Generic;
using System.Text;

namespace L3_DOTNET
{
    class Konto
    {
        public string PESEL { get; set; }
        public string NumerKonta { get; set; }

        public Konto(string p, string n)
        {
            PESEL = p;
            NumerKonta = n;
        }
    }
}
