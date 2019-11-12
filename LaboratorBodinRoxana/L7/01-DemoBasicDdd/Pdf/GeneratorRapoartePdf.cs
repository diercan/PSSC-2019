using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pdf
{
    /// <summary>
    /// Clasa care implementeaza operatii cu PDF-uri
    /// </summary>
    public class GeneratorRapoartePdf
    {
        public string GenerareRaportTabelar(List<List<string>> continut)
        {
            Console.WriteLine("Generare raport sub forma de tabel in format PDF.");
            foreach (var linie in continut)
            {
                foreach (var c in linie)
                {
                    Console.Write(c);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }
            return "c:\raport.pdf";
        }
    }
}
