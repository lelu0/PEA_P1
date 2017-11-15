using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    class Program
    {
        static void Main(string[] args)
        {
            //WorldMap wm = new WorldMap(@"C:\berlin52x\berlin52.xml");
            // wm.printCurrentMap();
            
           //ProblemInstance problemInstance = new ProblemInstance(new WorldMap(9));
           //problemInstance.Solve();
            Console.WriteLine("Podaj sciezke pliku xml:");
            string file = Console.ReadLine();
            Console.WriteLine("Czy startowe miasto w zapisie jest pominiete? true/false");
            string skip = Console.ReadLine();
            bool skiped;
            bool.TryParse(skip, out skiped);
            Tests test = new Tests(file,skiped);
            //Tests test = new Tests(30);
            //Console.WriteLine("Test for ftv33");
            test.DoSeriesTest(1, true, false ,false);
            Console.ReadLine();
        }
    }
}
