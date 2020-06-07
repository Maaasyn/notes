using System;
using System.Collections.Generic;
using System.Linq;

namespace lambdyMoje {
    public static class SzalonaMetoda {
        public static double Oblicz < T > (this T liczbaStartowa, T argumentDoDzialania, Func < T, T, double > metodaLiczaca) 
        {
            return metodaLiczaca(liczbaStartowa, argumentDoDzialania);
        }

        public static T DodajDoList < T > (this T argument, params IList<T>[] args) {
            foreach(var item in args){
                item.Add(argument);
            }
            return argument;
        }

        public static IEnumerable<T> WhereMarcinka<T> (this IEnumerable<T> enumerable, Predicate<T> sposob)
        {
            List<T> result = new List<T>();
            foreach (var item in enumerable)
            {
                if(sposob(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}