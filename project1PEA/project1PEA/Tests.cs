using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace project1PEA
{
    public class Tests
    {
        public ProblemInstance ProblemInstance { get; set; }
        public int Cities { get; set; }

        public Tests(string filePath)
        {
            ProblemInstance = new ProblemInstance(new WorldMap(filePath));
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

        public List<double> DoSeriesTest(int tests, bool printResults, bool generateNewMap)
        {
            List<double> results = new List<double>();
            for (int i = 0; i < tests; i++)
            {
                Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                ProblemInstance.Solve(false,false);
                watch.Stop();
                double elapsedMs = watch.ElapsedMilliseconds;
                if(printResults)
                    Console.WriteLine("Test " + i +": " + elapsedMs + " ms");
                results.Add(elapsedMs);
                if(generateNewMap)
                    ProblemInstance.WorldMap = new WorldMap(Cities);
            }
            
            return results;
        }
    }
}