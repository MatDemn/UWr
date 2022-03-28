using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace L3_DOTNET
{
    class ZAD_1_3_8
    {
        public static void Run()
        {
            List<int> list = new List<int>() { 1, 2, 3, 4, 5 };
            Func<int, int> f = null;

            Console.WriteLine("-------------Zadanie 1.3.8-------------");
            foreach (var item in
                list.Select(f = i => (i<=2) ? 1 : f(i-1) + f(i-2)))
                Console.WriteLine(item);
        }
    }
}
