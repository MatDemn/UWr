using System;
using System.Reflection;

namespace L2
{   
    class ZAD_1_1_3
    {
        public static void Run()
        {
            Console.WriteLine("---------------Zad 1.1.3---------------");

            Kwadrat kw = new Kwadrat(15, 20);

            // Przykład dostępu do pól prywatnych
            Console.WriteLine("Przykład dostępu do propercji i metod prywatnych przy pomocy refleksji:");
            PropertyInfo[] props = typeof(Kwadrat).GetProperties(
                         BindingFlags.NonPublic |
                         BindingFlags.Instance);
            MethodInfo[] methods = typeof(Kwadrat).GetMethods(
                        BindingFlags.NonPublic |
                        BindingFlags.Instance);

            foreach (PropertyInfo f in props)
            {
                Console.WriteLine("\tPropercja {0}: {1}", f, f.GetValue(kw));
            }

            foreach (MethodInfo m in methods)
            {
                Console.WriteLine("\tMetoda {0}", m);
            }

            int liczbaProb = 1000000;
            DateTime start;
            DateTime end;
            TimeSpan czas;

            start = DateTime.Now;
            // Dostęp zwykły do pól publicznych
            for (int i = 0; i < liczbaProb; i++)
            {
                var val = kw.example;
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("Dostęp do propercji (próby: {0}):", liczbaProb);
            Console.WriteLine("Czas dla dostępu zwykłego: {0}", czas);

            kw = new Kwadrat(15, 20);
            start = DateTime.Now;
            // Dostęp za pomocą Refleksji
            for (int i = 0; i<liczbaProb; i++)
            {
                var val = typeof(Kwadrat).GetProperty("example", BindingFlags.Public | BindingFlags.Instance).GetValue(kw);
            }
            end = DateTime.Now;
            czas = end - start;
            Console.WriteLine("Czas dla dostępu przez refleksje: {0}", czas);

        }
    }

}
