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
        //public int TabuLenght { get; set; }
        public int NumberOfTweaks { get; set; }
        public int Iterator { get; set; }
        public int LastChangeIteration { get; set; }
        public int RestartIteration { get; set; }
        public int DiversifyFactor { get; set; }
       // public int BufferSize { get; set; }
        public int Cadency { get; set; }
        public List<int> Solution { get; set; }
        public List<List<int>> SolutionsList { get; set; }
        public List<int[]> Path { get; set; }
        public List<TabuElement> TabuList { get; set; }
        private readonly Random _rn;
        public TabuSearch(int numberOfTweaks,int cadency = 0, int divFactor = 0, /*int bufferSize = 5,*/ int cities = 0, WorldMap wm = null)
        {
            WorldMap = cities == 0 ? wm : new WorldMap(cities);
            DiversifyFactor = divFactor != 0 ? divFactor : (int)Math.Floor((double)WorldMap.Cities*3.0);
            Cadency = cadency != 0 ? cadency : (int) Math.Floor((double) WorldMap.Cities * 3.0); 
            RestartIteration = 0;
            LastChangeIteration = 0;
            //TabuLenght = tabuLenght;
            //BufferSize = bufferSize;
            NumberOfTweaks = numberOfTweaks;
            Solution = new List<int>();
            Path = new List<int[]>();
            TabuList = new List<TabuElement>();
            _rn = new Random();
        }

        public void Solve()
        {
            //Step 1
            CreateBaseSolution();
            //Step 2
            var lastCandidate = Solution;
            while (Iterator < NumberOfTweaks) //TODO check finish statement
            {
                //Step 3
                var bestInNeighborhood = GetBestInNeighborhood(GenerateNeighborhood(new List<int>(lastCandidate)));
                //Step 4
                var changed = GetChangedPair(bestInNeighborhood, lastCandidate);
                if (SearchOnTabuList(changed))
                {
                    var aspirationCost = GetSolutionCost(bestInNeighborhood);
                    if (aspirationCost + aspirationCost / 30 < GetSolutionCost(Solution))
                        lastCandidate = SetNewGlobalBest(bestInNeighborhood, changed);
                }
                //Step 5
                else if (CompareSolutions(bestInNeighborhood))
                {
                    lastCandidate = SetNewGlobalBest(bestInNeighborhood, changed);
                }
                //Step 6
                VerifyTabuList();
                //Step 7
                if (CriticalEvent())
                {
                    //Step 8
                    lastCandidate = Restart();
                    //Step 9
                    if (CompareSolutions(lastCandidate))
                    {
                        TabuList.Add(new TabuElement(GetChangedPair(lastCandidate,Solution),Cadency));
                        Solution = new List<int>(lastCandidate);
                        LastChangeIteration = Iterator;
                    }
                }
                   
                Iterator++;
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

        public List<int> Restart() //if first candidate in buffer is equal current solution create random based solution
        {
            var output = new List<int>(Solution); //TODO Try from i = 0;
            for (int i = (int)Math.Floor((double)Solution.Count / 3); i < Solution.Count-1; i++)
            {
                output[i] = 0;
            }
            for (int i = (int)Math.Floor((double)Solution.Count / 3); i < Solution.Count - 1; i++)
            {
                do
                {
                  output[i] = _rn.Next(1, output.Count-1);
                } while (CheckPreviousApperance(i, output));
            }
            

            RestartIteration = Iterator;
            LastChangeIteration = Iterator;
            return output;
        }

        public bool CheckPreviousApperance(int index, List<int> listToCheck)
        {
            for (int i = 0; i < listToCheck.Count-1; i++)
            {
                if (i == index) continue;
                if (listToCheck[i] == listToCheck[index]) return true;
            }
            return false;
        }

        public List<List<int>> GenerateNeighborhood(List<int> solution) //generating by Vertex Exchange (VertexEx)
        {
            List<List<int>> hood = new List<List<int>>();
            for (int i = 1; i < solution.Count-1; i++) //First and last element of path are always the same, start vertex
            {
                for (int j = 1; j < solution.Count-1; j++)
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
                if (GetSolutionCost(solution) < currentBestValue) bestInHood = new List<int>(solution);
            }
            return bestInHood;
        }

        public double GetSolutionCost(List<int> solution)
        {
            var cost = 0.0;
            for (int i = 0; i < Solution.Count - 1; i++)
            {
                cost += WorldMap.CityMatrix[solution[i], solution[i + 1]];
            }
            return cost;
        }


        public void AddToTabuBuffer(List<int> solution) //FIFO buffer: 2,3,6.add(4) -> 3,6,4 
        {
            if (SolutionsList.Count >= 5)
                SolutionsList.RemoveAt(0);
            SolutionsList.Add(solution);
        }

        public bool CompareSolutions(List<int> candidate) //tru if candidate is better
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
        
        public List<int> SetNewGlobalBest(List<int> candidate, List<int> changed)
        {
            Solution = new List<int>(candidate);
            LastChangeIteration = Iterator;
            TabuList.Add(new TabuElement(new List<int>(changed), Cadency));
            return new List<int>(candidate);
        }
    }
}
