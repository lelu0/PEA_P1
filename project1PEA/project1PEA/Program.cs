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
           Dynamic dyn = new Dynamic();
            Console.WriteLine(dyn.SolveProblem());

        }
    }
}
