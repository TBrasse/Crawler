using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPKXMLCrawler
{
    static public class StaticHelpers
    {
        static public void ForEach<T>(this T[] array, Action<T> func)
        {
            Array.ForEach(array, func);
        }
    }
}
