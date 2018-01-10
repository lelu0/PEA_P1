using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    class GeneticAlgorithm
    {
        public Population Population;
        public Individual Best;
        public int PopulationSize;
        public int TournamentSize;
        public int NumberOfIterations;
        public WorldMap WorldMap;
   

        public GeneticAlgorithm(int populationSize, int tournamentSize, int numberOfIterations, WorldMap worldMap)
        {
            PopulationSize = populationSize;
            TournamentSize = tournamentSize;
            NumberOfIterations = numberOfIterations;
            WorldMap = worldMap;
        }

        public void Solve()
        {
            //Random initialization
            Population = new Population(PopulationSize,WorldMap); 
            var best = Population.GetRandomIndividual();
            //main loop
            var iterator = 0;
            while (iterator < NumberOfIterations) //TODO End condition - find better if possible
            {
                iterator++;
                Console.WriteLine(iterator + ";" + best.GetIndividualCost(Population.WorldMap.CityMatrix));
                //Generating next population
                var newPopulation = new List<Individual>();
                for (int i = 0; i < Population.Individuals.Count/2; i++)
                {
                    //Crossover population with parent selected on tournament
                    var childrens = Population.CrossOverIndividuals(Population.Tournament(TournamentSize));
                    //Mutation (10% of occur)
                    foreach (var children in childrens)
                    {
                        children.Mutation(Population.rn);
                    }
                    newPopulation.Add(childrens[0]);
                    newPopulation.Add(childrens[1]);
                }
                //assign new population 
                Population.Individuals = newPopulation;
                //Asssess Fitness
                var newCandidate = Population.GetBestIndividual();
                best = newCandidate.GetIndividualCost(Population.WorldMap.CityMatrix) <
                       best.GetIndividualCost(Population.WorldMap.CityMatrix)
                    ? newCandidate
                    : best;
            }
            Best = best;
        }
    }
}
