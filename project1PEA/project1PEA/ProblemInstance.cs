using System;
using System.Collections.Generic;

namespace project1PEA
{
    public class ProblemInstance
    {
        public WorldMap WorldMap { get; set; }
        
        public ProblemInstance(WorldMap worldMap)
        {
            WorldMap = worldMap;
        }
        public ProblemInstance(int cities)
        {
            WorldMap = new WorldMap(cities);
        }

        public void PrintPath(List<int[]> path)
        {
            foreach (var p in path)
            {
                Console.WriteLine(p[0] + " -> " + p[1]);
            }
        }

        public void Solve(bool printPath = true, bool printResult = true)
        {
            List<LiveNode> liveNodes = new List<LiveNode>();
            List<int[]> path = new List<int[]>();

            //creating of root node 
            liveNodes.Add(new LiveNode(path, WorldMap.CityMatrix, 0, -1, 0, WorldMap.Cities));

            //calculate LB of path
            liveNodes[0].StandarizeMatrix();

            while (liveNodes.Count != 0)
            {
                LiveNode minNode = MathUtils.GetMinCostNode(liveNodes); //find a node with min estimated cost
                var minNodeIndex = MathUtils.GetMinCostNodeIndex(liveNodes);
               // liveNodes[minNodeIndex] = null;
                liveNodes.RemoveAt(MathUtils.GetMinCostNodeIndex(liveNodes));
                int currentCity = minNode.Vertex;

                //if all city has been visited
                if (minNode.Level == WorldMap.Cities - 1)
                {
                    //return to start
                    minNode.Path.Add(new[] { currentCity, 0 });
                    if (printPath)
                        PrintPath(minNode.Path);
                    if (printResult)
                        Console.WriteLine("Optimal cost = " + minNode.Cost);
                    return;
                }

                //loop for each child
                for (int i = 0; i < WorldMap.Cities; i++)
                {
                    if (minNode.NodeMatrix[currentCity, i] != double.MaxValue)
                    {
                        //create a new node and calculate it cost
                        LiveNode childNode = new LiveNode(minNode.Path, minNode.NodeMatrix, minNode.Level + 1, currentCity, i, WorldMap.Cities);
                        //Child node cost = Cost to travel to previous city + cost of travel from prev city to child city + lower bound of child node (calculated while matrix is reduct)
                        childNode.StandarizeMatrix();
                        childNode.Cost += minNode.Cost + minNode.NodeMatrix[currentCity, i];
                        //add child to list
                        liveNodes.Add(childNode);
                    }
                }
            }
        }





    }
}