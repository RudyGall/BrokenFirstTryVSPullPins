using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPinterest
{
    class Program
    {
        static void Main(string[] args)
        {
            var pinterest = new Pinterest
            {
                AppID = "5021144650893801612",
                AppIDSecret = "ba3a73bd5aab2032c03535e2d0d29d059bc41a96d4efdf8e109ea32629fb2a95",
                AccessToken= "Ane9FdYP7YJNPdqqG8Rr5iTgW5utFYqud3VlVcBFrrB08mC4jAm_wDAAAiGTRa6zDAbAr7UAAAAA"

            };
            IEnumerable<string> pins = pinterest.GetPins("https://api.pinterest.com/v1/users/GcSocialmediatest/", "Gc", "Socialmediatest").Result;
            foreach (var p in pins)
            {
                Console.WriteLine(p + "\n");
            }
            Console.ReadKey();
        }
    }
}
