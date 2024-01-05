using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string i = "1";
            string j = i;
            i = "2";
            Console.WriteLine("{0},{1}", i, j);
        }
    }
}
