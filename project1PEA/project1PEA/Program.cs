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
               /* WorldMap map = new WorldMap(sampleMap);
                TabuSearch tabu = new TabuSearch(1000000, 0, 62, 0, map);
                Console.WriteLine("Start Solving for tabu ");
                tabu.Solve();
                tabu.SolutionToPath();
                tabu.PrintPath(tabu.Path);
                Console.WriteLine("Cost :" + tabu.GetSolutionCost(tabu.Solution));*/
            
           // Tests Test = new Tests(@"C:\ftv33.xml",false);
           // Test.DoSeriesTestTabu(5, true, false, true);
            
             //TEST UI - READ FROM FILE AND DO SINGLE TEST
           /* Console.WriteLine("Podaj sciezke pliku xml:");
            string file = Console.ReadLine();
            Console.WriteLine("Czy startowe miasto w zapisie jest pominiete? true/false");
            string skip = Console.ReadLine();
            bool skiped;
            bool.TryParse(skip, out skiped);
            Tests test = new Tests(file,skiped);
            //Tests test = new Tests(30);
            //Console.WriteLine("Test for ftv33");
            test.DoSeriesTestTabu(1, true, false ,false);
            */

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
            //Population p = new Population();
            //GeneticAlgorithm ga = new GeneticAlgorithm(500,10,300,new WorldMap(@"C:\gr21.xml"));
            GeneticTests gt = new GeneticTests(1500, 6, 300, new WorldMap(@"C:\gr21.xml")); //opt parameters for gr21, br 17
            /*
            Console.WriteLine("burma 14-------------------------------");
            for (int i = 1000; i <= 2000; i+= 500)
            {
                for (int j = 6; j < 50; j+=20)
                {
                    for (int k = 200; k < 500; k+=100)
                    {
                        Console.WriteLine(i + ";" + j + ";" + k);
                        gt = new GeneticTests(i, 6, 300, new WorldMap(@"C:\burma14.xml"));
                        var ang = gt.GetAverageCost(20);
                        Console.WriteLine(ang);
                    }
                    
                }
            }
            Console.WriteLine("gr24-------------------------------");
            for (int i = 1000; i <= 2000; i += 500)
            {
                for (int j = 6; j < 50; j += 20)
                {
                    for (int k = 200; k < 500; k += 100)
                    {
                        Console.WriteLine(i + ";" + j + ";" + k);
                        gt = new GeneticTests(i, 6, 300, new WorldMap(@"C:\gr24.xml"));
                        var ang = gt.GetAverageCost(20);
                        Console.WriteLine(ang);
                    }

                }
            }
            
            Console.WriteLine("ftv33-------------------------------");
            for (int i = 1000; i <= 2000; i += 500)
            {
                for (int j = 6; j < 50; j += 20)
                {
                    for (int k = 200; k < 500; k += 100)
                    {
                        Console.WriteLine(i + ";" + j + ";" + k);
                        gt = new GeneticTests(i, j, k, new WorldMap(@"C:\ftv33.xml",false));
                        var ang = gt.GetAverageCost(20);
                        Console.WriteLine(ang);
                    }

                }
            }
            Console.WriteLine("ftv38-------------------------------");
            for (int i = 1000; i <= 2000; i += 500)
            {
                for (int j = 6; j < 50; j += 20)
                {
                    for (int k = 200; k < 500; k += 100)
                    {
                        Console.WriteLine(i + ";" + j + ";" + k);
                        gt = new GeneticTests(i, j, k, new WorldMap(@"C:\ftv38.xml",false));
                        var ang = gt.GetAverageCost(20);
                        Console.WriteLine(ang);
                    }

                }
            }
            Console.WriteLine("p43-------------------------------");
            for (int i = 1000; i <= 2000; i += 500)
            {
                for (int j = 6; j < 50; j += 20)
                {
                    for (int k = 200; k < 500; k += 100)
                    {
                        Console.WriteLine(i + ";" + j + ";" + k);
                        gt = new GeneticTests(i, j, k, new WorldMap(@"C:\p43.xml",false));
                        var ang = gt.GetAverageCost(20);
                        Console.WriteLine(ang);
                    }

                }
            }
            */
            
       
            //TEST UI - READ FROM FILE AND DO SINGLE TEST
             Console.WriteLine("Podaj sciezke pliku xml:");
             string file = Console.ReadLine();
             Console.WriteLine("Czy startowe miasto w zapisie jest pominiete? true/false");
             string skip = Console.ReadLine();
             Console.WriteLine("Podaj wielkosc populacji");
            string i = Console.ReadLine();
            Console.WriteLine("Podaj wielkosc puli turniejowej");
            string t = Console.ReadLine();
            Console.WriteLine("Podaj ilosc pokolen");
            string p = Console.ReadLine();
            Console.WriteLine("Podaj ilosc wykonan do usrednienia wyniku");
            string n = Console.ReadLine();
            bool skiped;
            bool.TryParse(skip, out skiped);
            gt = new GeneticTests(int.Parse(i), int.Parse(t), int.Parse(p), new WorldMap(file,skiped));
            var ang = gt.GetAverageCost(int.Parse(n));
            Console.WriteLine(ang);
            Console.ReadLine();


        }
    }
}
