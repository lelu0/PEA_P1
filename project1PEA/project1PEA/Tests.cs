using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace project1PEA
{
    public class Tests
    {
        public ProblemInstance ProblemInstance { get; set; }
        public int Cities { get; set; }

        public Tests(string filePath, bool skipStartCity = true)
        {
            ProblemInstance = new ProblemInstance(new WorldMap(filePath, skipStartCity));
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
            if(generateResultCsv)
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
                if(generateResultCsv)
                    resultFile.WriteLine(elapsedMs);
                if (generateNewMap)
                    ProblemInstance.WorldMap = new WorldMap(Cities);
            }
            if(generateResultCsv) resultFile.Close();
            return results;
        }
    }
}