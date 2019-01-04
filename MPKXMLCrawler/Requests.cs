using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPKXMLCrawler
{
    public static class Requests
    {
        public enum Types
        {
            Lines,
            Schedule
        }

        public static (string,string)[] GetRequest(Types type)
        {
            switch (type)
            {
                case Types.Lines:
                    return new[]
                    {
                        ("lang", Language),
                        ("akcja", "index"),
                        ("rozklad", Date)
                    };
                case Types.Schedule:
                    return new[] 
                    {
                        ("lang", Language),
                        ("rozklad", Date),
                        ("linia", Line)
                    };
                default:
                    throw new NotSupportedException($"Request type is not supported: {type}");
            }
        }

        public static string Date => DateTime.Now.ToString("yyyyMMdd");
        public static string Line => "1";
        public static string Language => "PL";
    }
}
