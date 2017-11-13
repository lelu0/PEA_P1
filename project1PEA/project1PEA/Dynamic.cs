using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    public class Dynamic
    {
        public List<List<int>> Combinations;
        public int CitiesNumber { get; set; }
        public WorldMap WorldMap { get; set; }
        public List<Element> Elements { get; set; }
        public Dynamic()
        {
            Combinations = new List<List<int>>();
            WorldMap = new WorldMap(4);
            GenerateAllCombinations(WorldMap.Cities - 1);
            Elements = new List<Element>();
            CitiesNumber = WorldMap.Cities;
        }

        public double SolveProblem()
        {
            Elements = new List<Element>();
            foreach (var combination in Combinations)
            {
                for (int i = 0; i < CitiesNumber; i++)//cv
                {
                    if(combination.Contains(i)) continue;

                    double minCost = double.MaxValue;
                    int minParent = 0;
                    List<int> copy = new List<int>(combination);
                    foreach (var v in combination)//pv
                    {
                        double cost = WorldMap.CityMatrix[v, i] + GetCost(copy, v);
                        if (cost < minCost)
                        {
                            minCost = cost;
                            minParent = v;
                        }
                    }

                    //empty set
                    if (combination.Count == 0) minCost = WorldMap.CityMatrix[0, i];

                    Elements.Add(new Element(i,combination,minCost,minParent));
                }
            }

            //Another stage
            List<int> set = new List<int>();
            for (int i = 1; i < WorldMap.Cities; i++)
            {
                set.Add(i);
            }

            double min = double.MaxValue;
            int prevVertex = -1;

            List<int> copySet = new List<int>(set);
            foreach (var v in set )
            {
                double cost = WorldMap.CityMatrix[v, 0] + GetCost(copySet, v);
                if (cost < min)
                {
                    min = cost;
                    prevVertex = v;
                }
            }

            return min;
        }

        
        public double GetCost(List<int> combination, int prevVertex)
        {
            combination.Remove(prevVertex);
            double cost = 0;
            foreach (var e in Elements)
            {
                if (e.IsEqual(prevVertex, combination)) cost = e.Cost;
            }
            combination.Add(prevVertex);
            return cost;

        }

        
        public void GenerateAllCombinations(int cities)
        {
            
            int[] citiesToVisit = new int[cities];
            for (int i = 0; i < cities; i++) //fill table with cities from 1 ... n
            {
                citiesToVisit[i] = i + 1;
            }
            int[] result = new int[cities];
            GenerateCombinationRecursive(citiesToVisit, 0, 0, result);
            Combinations.Sort((a,b) => a.Count.CompareTo(b.Count));

        }

        private void GenerateCombinationRecursive(int[] citiesToVisit, int start, int level, int[] result)
        {
            if (level == citiesToVisit.Length) return;
            Combinations.Add(GenerateList(result,level));
            for (int i = start; i < citiesToVisit.Length; i++)
            {
                result[level] = citiesToVisit[i];
                GenerateCombinationRecursive(citiesToVisit,i+1,level+1,result);

            }
        }

        public List<int> GenerateList(int[] citiesToVisit, int level)
        {
            if (level == 0) return new List<int>();
            List<int> list = new List<int>();
            for (int i = 0; i < level; i++)
            {
                list.Add(citiesToVisit[i]);
            }
            return list;
        }

    }
}

