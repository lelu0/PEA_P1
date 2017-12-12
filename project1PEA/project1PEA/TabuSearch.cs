using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    class TabuSearch : ProblemInstance
    {
        public int TabuLenght { get; set; }
        public int NumberOfTweaks { get; set; }
        public int LastChangeIteration { get; set; }
        public int Iterator { get; set; }
        public int RestartIteration { get; set; }
        public int DiversifyFactor { get; set; }
        public int BufferSize { get; set; }
        public List<int> Solution { get; set; }
        public List<int> BaseSolution { get; set; }
        public List<List<int>> SolutionsList { get; set; }
        public List<int[]> Path { get; set; }
        public List<TabuElement> TabuList { get; set; }

        public TabuSearch(int tabuLenght, int numberOfTweaks,int bufferSize = 5, int cities = 0, WorldMap wm = null)
        {
            WorldMap = cities == 0 ? wm : new WorldMap(cities);
            RestartIteration = 0;
            LastChangeIteration = 0;
            TabuLenght = tabuLenght;
            BufferSize = bufferSize;
            NumberOfTweaks = numberOfTweaks;
            Solution = new List<int>();
            BaseSolution = new List<int>();
            Path = new List<int[]>();
            WorldMap = new WorldMap(cities);
        }

        public void Solve()
        {
            //Step 1
            CreateBaseSolution();
            //Step 2
            var lastCandidate = Solution;
            while (true) //TODO finish statement
            {
                //Step 3
                var bestInNeighborhood= GetBestInNeighborhood(GenerateNeighborhood(lastCandidate));
                //Step 4
                var changed = GetChangedPair(bestInNeighborhood,lastCandidate);
                if (SearchOnTabuList(changed))
                {
                   
                }


            }
        }

        public void CreateBaseSolution()
        {
            Solution = SalesmanAlgorithm.Greedy(WorldMap.CityMatrix, WorldMap.Cities);
        }

        public void SolutionToPath()
        {
            for (int i = 0; i < Solution.Count - 1; i++)
            {
                Path.Add(new[] { Solution[i], Solution[i + 1] });
            }
        }

        public void VerifyTabuList()
        {
            for (int i = 0; i < TabuList.Count; i++)
            {
                if (--TabuList[i].Cadence == 0)
                    TabuList.RemoveAt(i);
            }
        }

        public bool CriticalEvent()
        {
            return (Iterator - LastChangeIteration > DiversifyFactor || Iterator - RestartIteration > DiversifyFactor);
        }

        public void Restart() //if first candidate in buffer is equal current solution create random based solution
        {
            if (Solution != SolutionsList[0])
            {
                Solution = SolutionsList[0];
                AddToTabuBuffer(Solution);
            }
            else
            {
                Random rn  = new Random();
                for (int i = (int)Math.Floor((double)Solution.Count / 2); i < Solution.Count; i++)
                {
                    do
                    {
                        Solution[i] = rn.Next(0, Solution.Count - 1);
                    } while (CheckPreviousApperance(i));
                }
            }
        }

        public bool CheckPreviousApperance(int index)
        {
            for (int i = 0; i < index; i++)
            {
                if (Solution[i] == Solution[index]) return true;
            }
            return false;
        }

        public List<List<int>> GenerateNeighborhood(List<int> solution) //generating by Vertex Exchange (VertexEx)
        {
            List<List<int>> hood = new List<List<int>>();
            for (int i = 0; i < solution.Count; i++)
            {
                for (int j = 0; j < solution.Count; j++)
                {
                    if (i == j) continue;
                    List<int> tmpSolution = new List<int>(solution);
                    var toSwapElement = solution[i];
                    tmpSolution[i] = tmpSolution[j];
                    tmpSolution[j] = toSwapElement;
                    hood.Add(new List<int>(tmpSolution));
                }
            }
            return hood;
        }

        public List<int> GetBestInNeighborhood(List<List<int>> hood)
        {
            List<int> bestInHood = new List<int>();
            var currentBestValue = double.MaxValue;
            foreach (var solution in hood)
            {
                if(GetSolutionCost(solution) < currentBestValue) bestInHood = new List<int>(solution);
            }
            return bestInHood;
        }

        public double GetSolutionCost(List<int> solutions)
        {
            var cost = 0.0;
            for (int i = 0; i < Solution.Count - 1; i++)
            {
                cost += WorldMap.CityMatrix[Solution[i], Solution[i + 1]];
            }
            return cost;
        }

        
        public void AddToTabuBuffer(List<int> solution) //FIFO buffer: 2,3,6.add(4) -> 3,6,4 
        {
            if (SolutionsList.Count >= 5)
                SolutionsList.RemoveAt(0);
            SolutionsList.Add(solution);
        }

        public bool CompareSolutions(List<int> candidate)
        {
            return GetSolutionCost(Solution) > GetSolutionCost(candidate);
        }

        public List<int> GetChangedPair(List<int> candidate, List<int> lastCandidate)
        {
            var pair = new List<int>();
            for (int i = 0; i < candidate.Count; i++)
            {
                if (candidate[i] != lastCandidate[i])
                {
                    pair.Add(candidate[i]);
                }
            }
            return pair;

        }

        public bool SearchOnTabuList(List<int> pair)
        {
            foreach (var tabuElement in TabuList)
            {
                if (tabuElement.IsEqual(pair)) return true;
            }
            return false;
        }

        public bool CheckAspiration(List<int> pair)
        {
            return (WorldMap.CityMatrix[pair[0],pair[0]+1])
        }
    }
}
