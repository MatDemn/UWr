using System;
using System.Collections.Generic;
using System.Text;
using  Microsoft.Office.Interop.Word;

namespace DotNet_4
{
    static class Utils
    {
        // Do zadania 1
        public static int Make1(int x, int y)
        {
            return (x * x + y * y);
        }

        public static int Make2(dynamic x, dynamic y)
        {
            return (x * x + y * y);
        }

        // Do zadania 2

        public static void saveInWordDocument()
        {
              
            Application winword = new Application();

            winword.ShowAnimation = false;

            winword.Visible = false;

            object missing = System.Reflection.Missing.Value;

            Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            document.Content.SetRange(0, 0);
            document.Content.Text = "Programowanie pod Windows" + Environment.NewLine;
 
            object filename = @"C:\DOTNET_L4Z2.docx";
            document.SaveAs(ref filename);
            document.Close(ref missing, ref missing, ref missing);
            document = null;
            winword.Quit(ref missing, ref missing, ref missing);
            winword = null;
        }
    }
}
