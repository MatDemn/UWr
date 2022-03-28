using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace L2
{
    class ZAD_1_1_4
    {
        public static void znajdzUruchom(object o)
        {
            MethodInfo[] mInfo = o.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var res = from elem in mInfo
                      where
                        elem.ReturnType == typeof(int) &&
                        elem.GetParameters().Length == 0 &&
                        elem.GetCustomAttributes(typeof(Oznakowane)).Count() > 0
                      select elem;
            foreach (MethodInfo m in res)
            {
                Console.WriteLine("Nazwa metody: {0}", m.ToString());
                Console.WriteLine("Wynik działania: {0}", m.Invoke(o, null));
            }
        }

        public static void Run()
        {
            Console.WriteLine("---------------Zad 1.1.4---------------");
            Kwadrat kw = new Kwadrat(20, 15);
            Console.WriteLine("Wynik wywołania metod zgodnych z warunkami zadania dla obiektu klasy {0}", kw.GetType().ToString());
            znajdzUruchom(kw);
            Console.WriteLine("Jeśli nie wyświetla się żadna metoda Foo, to funkcja działa dobrze.");
        }
        
    }
}
