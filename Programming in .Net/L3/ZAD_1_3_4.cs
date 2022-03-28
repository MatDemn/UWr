using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace L3_DOTNET
{
    class ZAD_1_3_4
    {
        public static void Run()
        {
            DirectoryInfo fold = new DirectoryInfo(@"..\..\..\");
            long policz = (from f in fold.EnumerateFiles()
                          select f.Length).Aggregate((a, b) => a + b);

            Console.WriteLine("-------------Zadanie 1.3.4-------------");
            Console.WriteLine("Suma długości plików w folderze z plikami .cs {0}", policz);
        }
    }
}
