using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    class Individual
    {
        public List<int> Path;

        public Individual(List<int> p)
        {
            this.Path = p;
        }

        public Individual(int pathLength)
        {
            Path = new List<int>();
            for (int i = 1; i < pathLength-1; i++) //skip begining and and
            {
                Path.Add(int.MaxValue);
            }
            //Adding begining and end of route
            Path.Insert(0,0);
            Path.Insert(pathLength-1,0);
        }

        public Individual() { }

        public Individual(Random rn, int pathLength)
        {
            Path = new List<int>();
            Path.Add(0);
            while (Path.Count < pathLength-1)
            {
                var rand = rn.Next(1, pathLength - 1);
                if(Path.IndexOf(rand) == -1)
                    Path.Add(rand);
            }
            Path.Add(0);
        }
        public void Mutation(Random rn)
        {
            if (rn.Next(0, 101) > 10) return;
            var positions = new int[] { rn.Next(1, Path.Count - 1), rn.Next(1, Path.Count - 1) };
            var tmp = Path[positions[0]];
            Path[positions[0]] = Path[positions[1]];
            Path[positions[1]] = tmp;
        }

        public double GetIndividualCost(double[,] cityMatrix)
        {
            var cost = 0.0;
            for (int i = 0; i < Path.Count - 1; i++)
            {
                cost += cityMatrix[Path[i], Path[i + 1]];
            }
            return cost;
        }

        public void PrintPath(List<int> path)
        {
            foreach (var p in path)
            {
                Console.Write(p + " -> " );
            }
            Console.WriteLine();
        }
       
    }
}
