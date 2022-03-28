using System;
using System.Collections.Generic;
using System.Text;

namespace L3_DOTNET
{
    static class ZAD_1_3_1
    {
        public static bool isPalindrome(this string s)
        {
            int x, y;

            for(x = 0, y= s.Length - 1; x<y; x++, y--)
            {
                while (char.IsWhiteSpace(s[x]) || char.IsPunctuation(s[x])) x++;
                while (char.IsWhiteSpace(s[y]) || char.IsPunctuation(s[y])) y--;

                if (char.ToLower(s[x]) != char.ToLower(s[y])) return false;
            }
            return true;
        }

        public static void Run()
        {
            string s1 = "Do geese see God? ", s2 = "Madam, I’m Adam", s3 = "A Toyota! Race fast... safe car: a Toyota", 
                s4 = "Rats live on no evil star", s5 = "Are we not pure? “No, sir!” Panama’s moody Noriega brags. “It is garbage!” Irony dooms a man—a prisoner up to new era.";

            Console.WriteLine("-------------Zadanie 1.3.1-------------");
            Console.WriteLine("Program ma sprawdzać czy dany ciąg znaków (nie licząc spacji i znaków przestankowych) jest palindromem.");
            Console.WriteLine("Wszystkie poniższe słowa powinny zwrócić prawdę jeśli algorytm jest poprawny.");

            Console.WriteLine("\t" + s1);
            Console.WriteLine("\t" + isPalindrome(s1).ToString());
            Console.WriteLine("\t" + s2);
            Console.WriteLine("\t" + isPalindrome(s2).ToString());
            Console.WriteLine("\t" + s3);
            Console.WriteLine("\t" + isPalindrome(s3).ToString());
            Console.WriteLine("\t" + s4);
            Console.WriteLine("\t" + isPalindrome(s4).ToString());
            Console.WriteLine("\t" + s5);
            Console.WriteLine("\t" + isPalindrome(s5).ToString());

        }
    }
}
