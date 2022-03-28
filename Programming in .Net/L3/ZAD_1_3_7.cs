using System;
using System.Collections.Generic;
using System.Text;

namespace L3_DOTNET
{
    class ZAD_1_3_7
    {
        public static void Run()
        {
            var item = new { Field1 = "The value", Field2 = 42 };
            List<dynamic> theList = new List<dynamic>();

            for(int i = 0; i< 10; i++)
            {
                theList.Add(new { Field1 = "The value", Field2 = i });
            }

            Console.WriteLine("-------------Zadanie 1.3.7-------------");
            foreach (dynamic elem in theList)
            {
                Console.WriteLine(elem.Field1 + ": " + elem.Field2);
            }
            Console.WriteLine();
            
        }
    }
}
