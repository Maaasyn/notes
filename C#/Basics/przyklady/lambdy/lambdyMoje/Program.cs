using System;
using System.Collections.Generic;

namespace lambdyMoje
{
    class Program
    {
        static void Main(string[] args)
        {
            var lista1 = new List<double>{};
            var lista2 = new List<double>{};
            var lista3 = new List<double>{};


             var wynikDodawania = 6.Oblicz(5, (x,y) => x+x+x+x+x+y);

             5.Oblicz(5, (x,y) => x + y).DodajDoList(lista1).DodajDoList(lista1);

             foreach (var item in lista1)
             {
                 System.Console.WriteLine(item);
             }


            Console.WriteLine(wynikDodawania);

            var listaLiczb = new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};

System.Console.WriteLine("przerwa przerwa przerwa");
            var wynik = listaLiczb.WhereMarcinka(x => x > 5);

            foreach (var item in wynik)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}
