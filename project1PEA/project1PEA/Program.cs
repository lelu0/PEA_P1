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
            /* //TEST UI - READ FROM FILE AND DO SINGLE TEST
            Console.WriteLine("Podaj sciezke pliku xml:");
            string file = Console.ReadLine();
            Console.WriteLine("Czy startowe miasto w zapisie jest pominiete? true/false");
            string skip = Console.ReadLine();
            bool skiped;
            bool.TryParse(skip, out skiped);
            Tests test = new Tests(file,skiped);
            //Tests test = new Tests(30);
            //Console.WriteLine("Test for ftv33");
            test.DoSeriesTest(1, true, false ,false);*/

            /* //TEST FOR INSTANCE FROM TSPLIB
            Tests test = new Tests(@"C:\gr17.xml",true);
            test.DoSeriesTest(50, false, false, true);
            */
            WorldMap wm = new WorldMap(20);
            TabuSearch tbSearch = new TabuSearch(wm,2,2);
            tbSearch.TabuList = new List<TabuElement>();
            tbSearch.TabuList.Add(new TabuElement(3,2));
            tbSearch.TabuList.Add(new TabuElement(2,1));
            tbSearch.VerifyTabuList();
            tbSearch.CreateBaseSolution();
            Console.WriteLine("Done");
            tbSearch.SolutionToPath();
            tbSearch.PrintPath(tbSearch.Path);

            Console.ReadLine();
        }
    }
}
