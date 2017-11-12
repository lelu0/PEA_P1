using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    public class Dynamic
    {
        public List<List<int>> Combinations = new List<List<int>>();
        public int CitiesNumber { get; set; }

        public Dynamic(int cities)
        {
            Combinations = new List<List<int>>();
            CitiesNumber = cities;
            GenerateAllCombinations(cities-1);
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
