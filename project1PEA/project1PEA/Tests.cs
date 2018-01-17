using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace project1PEA
{
    public class Tests
    {
        public ProblemInstance ProblemInstance { get; set; }
        public TabuSearch TabuSearch { get; set; }
        public int Cities { get; set; }
        private WorldMap _map;

        public Tests(string filePath, bool skipStartCity = true, bool tabu = true)
        {
            if (!tabu)
                ProblemInstance = new ProblemInstance(new WorldMap(filePath, skipStartCity));
            else
            {
                _map = new WorldMap(filePath,skipStartCity);
                TabuSearch = new TabuSearch(100000, 0, 50, 0, _map);
            }
            
        }



        public Tests(int cities)
        {
            ProblemInstance = new ProblemInstance(new WorldMap(cities));
            Cities = cities;
        }
        public double DoSingleTest()
        {
            Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            ProblemInstance.Solve();
            watch.Stop();
            double elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }

        public List<double> DoSeriesTest(int tests, bool printResults, bool generateNewMap, bool generateResultCsv)
        {
            List<double> results = new List<double>();
            StreamWriter resultFile = null;
            if (generateResultCsv)
                resultFile = new StreamWriter("result.csv");
            for (int i = 0; i < tests; i++)
            {
                Console.WriteLine("Start test " + i + " at " + DateTime.Now);
                Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                ProblemInstance.Solve(printResults, printResults);
                watch.Stop();
                double elapsedMs = watch.ElapsedMilliseconds;
                if (printResults)
                    Console.WriteLine("Test " + i + ": " + elapsedMs + " ms" + " at " + DateTime.Now);
                results.Add(elapsedMs);
                if (generateResultCsv)
                    resultFile.WriteLine(elapsedMs);
                if (generateNewMap)
                    ProblemInstance.WorldMap = new WorldMap(Cities);
            }
            if (generateResultCsv) resultFile.Close();
            return results;
        }

        public List<double> DoSeriesTestTabu(int tests, bool printResults, bool generateNewMap, bool generateResultCsv)
        {
            List<double> results = new List<double>();
            StreamWriter resultFile = null;
            if (generateResultCsv)
                resultFile = new StreamWriter("result.csv");
            for (int i = 0; i < tests; i++)
            {
                Console.WriteLine("Start test " + i + " at " + DateTime.Now);
                Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                TabuSearch.Solve();
                watch.Stop();
                double elapsedMs = watch.ElapsedMilliseconds;
                if (printResults)
                {
                    TabuSearch.SolutionToPath();
                    TabuSearch.PrintPath(TabuSearch.Path);
                    Console.WriteLine("Test " + i + ": " + elapsedMs + " ms" + " at " + DateTime.Now + "Result: " +
                                      TabuSearch.GetSolutionCost(TabuSearch.Solution));
                }
                results.Add(elapsedMs);
                if (generateResultCsv)
                    resultFile.WriteLine(elapsedMs+","+ TabuSearch.GetSolutionCost(TabuSearch.Solution));
                if (generateNewMap)
                    TabuSearch.WorldMap = new WorldMap(TabuSearch.WorldMap.Cities);
                else
                    TabuSearch = new TabuSearch(100000, 0, 50, 0, _map);
                
            }
            if (generateResultCsv) resultFile.Close();
            return results;
        }

    }
}