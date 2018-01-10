using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    class Population
    {
        const double INF = double.MaxValue;
        public Random rn;
        public List<Individual> Individuals;
        public int PathLength;
        public WorldMap WorldMap;
        public Population()
        {
            WorldMap = new WorldMap(new double[5, 5]
            {
                {INF,1,9,1,8 },
                {1,INF,3,6,3 },
                {9,3,INF,2,4 },
                {1,6,2,INF,7 },
                {8,3,4,7, INF}
            });
            rn = new Random();
            Individuals = new List<Individual>()
            {
                new Individual(new List<int>() { 0,1,2,3,4,0}),
                new Individual(new List<int>() { 0,4,3,2,1,0})
            };
            PathLength = Individuals[0].Path.Count;
            CrossOverIndividuals(Individuals.ToArray());
        }

        public Population(int size)
        {
            WorldMap = new WorldMap(new double[5, 5]
            {
                {INF,1,9,1,8 },
                {1,INF,3,6,3 },
                {9,3,INF,2,4 },
                {1,6,2,INF,7 },
                {8,3,4,7, INF}
            });
            rn = new Random();
            Individuals = new List<Individual>();
            for (int i = 0; i < size; i++)
            {
                Individuals.Add(new Individual(rn,WorldMap.Cities+1));
            }
            PathLength = Individuals[0].Path.Count;
        }

        public Population(int size, WorldMap wm)
        {
            WorldMap = wm;
            rn = new Random();
            Individuals = new List<Individual>();
            for (int i = 0; i < size; i++)
            {
                Individuals.Add(new Individual(rn, WorldMap.Cities + 1));
            }
            PathLength = Individuals[0].Path.Count;
        }

        public Individual[] CrossOverIndividuals(Individual[] parents)
        {

            var indexes = GetCrossingIndexes(PathLength);
            var childrens = new Individual[2] {new Individual(PathLength), new Individual(PathLength) };

            for (int i = indexes[0]; i <= indexes[1]; i++) //copy parents section
            {
                childrens[0].Path[i] = parents[1].Path[i];
                childrens[1].Path[i] = parents[0].Path[i];
            }
            //Tested, works

            //copy rest of parent to childrends
            var childPathIndex = new int[]{indexes[1] + 1, indexes[1] + 1 };
            //upper path part
            for (int i = indexes[1]+1; i < PathLength; i++)
            {
                if (childrens[0].Path.IndexOf(parents[0].Path[i]) == -1)
                {
                    childrens[0].Path[childPathIndex[0]] = parents[0].Path[i];
                    childPathIndex[0] = CheckIndexOverflow(childPathIndex[0]);
                }
                if (childrens[1].Path.IndexOf(parents[1].Path[i]) == -1)
                {
                    childrens[1].Path[childPathIndex[1]] = parents[1].Path[i];
                    childPathIndex[1] = CheckIndexOverflow(childPathIndex[1]);
                }
            }
            //lower path part
            for (int i = 1; i <= indexes[1]; i++)
            {
                if (childrens[0].Path.IndexOf(parents[0].Path[i]) == -1)
                {
                    childrens[0].Path[childPathIndex[0]] = parents[0].Path[i];
                    childPathIndex[0] = CheckIndexOverflow(childPathIndex[0]);
                }
                if (childrens[1].Path.IndexOf(parents[1].Path[i]) == -1)
                {
                    childrens[1].Path[childPathIndex[1]] = parents[1].Path[i];
                    childPathIndex[1] = CheckIndexOverflow(childPathIndex[1]);
                }
            }
            return childrens;
            //Tested, OK
        }
        

        private int[] GetCrossingIndexes(int length)
        {

            var indexes = new int[2];
            indexes[0] = rn.Next(1, length - 2);
            while (true)
            {
                var tmp = rn.Next(1, length - 2);
                if (tmp > indexes[0])
                {
                    indexes[1] = tmp;
                    return indexes;
                }
                if (tmp < indexes[0])
                {
                    indexes[1] = indexes[0];
                    indexes[0] = tmp;
                    return indexes;
                }
            }
        }

        private int CheckIndexOverflow(int current)
        {
            return current + 1 < PathLength - 1 ? current + 1 : 1;
        }

        public Individual GetRandomIndividual()
        {
            return Individuals[rn.Next(0, Individuals.Count)];
        }

        public Individual[] Tournament(int tournamentSize)
        {
            var selected = new Individual[2]{GetRandomIndividual(),GetRandomIndividual()};
            //sort individuals on list

            if (selected[0].GetIndividualCost(WorldMap.CityMatrix) > selected[1].GetIndividualCost(WorldMap.CityMatrix))
            {
                var tmp = selected[1];
                selected[1] = selected[0];
                selected[0] = tmp;
            }
            for (int i = 2; i < tournamentSize; i++)
            {
                var candidate = GetRandomIndividual();
                if (candidate.GetIndividualCost(WorldMap.CityMatrix) <
                    selected[0].GetIndividualCost(WorldMap.CityMatrix))
                {
                    selected[1] = selected[0];
                    selected[0] = candidate;
                }
            }
            return selected;
        }

        public Individual GetBestIndividual()
        {
            var tmp = Individuals[0];
            foreach (var individual in Individuals)
            {
                if (individual.GetIndividualCost(WorldMap.CityMatrix) < tmp.GetIndividualCost(WorldMap.CityMatrix))
                    tmp = individual;
            }
            return tmp;
        }
    }
}
