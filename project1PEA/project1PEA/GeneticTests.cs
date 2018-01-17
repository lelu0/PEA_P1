using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    class GeneticTests
    {
        private GeneticAlgorithm GeneticAlgorith;

        public GeneticTests(int populationSize, int tournamentSize, int numberOfIterations, WorldMap worldMap)
        {
            GeneticAlgorith = new GeneticAlgorithm(populationSize,tournamentSize,numberOfIterations,worldMap);
        }

        public double SingleTest()
        {
            GeneticAlgorith.Solve();
            return GeneticAlgorith.Best.GetIndividualCost(GeneticAlgorith.WorldMap.CityMatrix);
        }

        public double GetAverageCost(int testsNumer)
        {
            Console.WriteLine("Doing avarage cost tests, please wait");
            var cost = 0.0;
            for (int i = 0; i < testsNumer; i++)
            {
                GeneticAlgorith.Solve();
                cost += GeneticAlgorith.Best.GetIndividualCost(GeneticAlgorith.WorldMap.CityMatrix);
                Console.Write(".");
            }
            return cost / testsNumer;
        }

        public double GetAverageTime(int testsNumer)
        {
            Stopwatch watch = new System.Diagnostics.Stopwatch();
            var time = 0.0;
            for (int i = 0; i < testsNumer; i++)
            {
                watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                GeneticAlgorith.Solve();
                watch.Stop();
                time += watch.ElapsedMilliseconds;
                Console.Write(".");
            }
            return time / testsNumer;
        }
    }
}
