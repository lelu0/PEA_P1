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
            if (Solution == SolutionsList[0])
            {
                Solution = SolutionsList[0];
                MoveSolutionsBuffer();
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

        public void MoveSolutionsBuffer() //FIFO buffer: 2,3,6.add(4) -> 3,6,4 
        {
            if (SolutionsList.Count >= 5)
                SolutionsList.RemoveAt(0);
            SolutionsList.Add(Solution);
        }
    }
}
