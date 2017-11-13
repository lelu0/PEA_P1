using System.Collections.Generic;
using System.IO;

namespace project1PEA
{
    public class LiveNode
    {
        public double[,] NodeMatrix { get; set; }
        public double Cost { get; set; } //store LB
        public int Vertex { get; set; } //Current level vertex
        public int Level { get; set; } //how many cities was visited
        public List<int[]> Path { get; set; }   
        public LiveNode(List<int[]> lowerPath, double[,] lowerMatrix, int l, int start, int end, int cities )
        {
            Path = lowerPath;
            if (l != 0)
                Path.Add(new []{start,end});
            NodeMatrix = lowerMatrix;
            for (int i = 0; l != 0 && i < cities ; i++)
            {
                NodeMatrix[start, i] = double.MaxValue;
                NodeMatrix[i, end] = double.MaxValue;
            }
            NodeMatrix[end, 0] = double.MaxValue;
            Level = l;
            Vertex = end;

        }
    }
}