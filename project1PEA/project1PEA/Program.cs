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
            const double INF = double.MaxValue;
            var sampleMap = new double[5, 5]
            {
                {INF,1,9,1,8 },
                {1,INF,3,6,3 },
                {9,3,INF,2,4 },
                {1,6,2,INF,7 },
                {8,3,4,7, INF}
            };
            for (int i = 200000; i < 500000; i+= 100000)
            {
                WorldMap map = new WorldMap(@"C:\br17.xml", false);
                TabuSearch tabu = new TabuSearch(i, 0, 45, 0, map);
                Console.WriteLine("Start Solving for tabu" + i);
                tabu.Solve();
                tabu.SolutionToPath();
                tabu.PrintPath(tabu.Path);
                Console.WriteLine("Cost :" + tabu.GetSolutionCost(tabu.Solution));
            }
            
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
            /*
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
            */
            Console.ReadLine();
        }
    }
}
