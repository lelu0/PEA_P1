using System;
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
            Path = new List<int[]>(lowerPath);
            if (l != 0)
                Path.Add(new []{start,end});
            NodeMatrix = (double[,]) lowerMatrix.Clone();
            for (int i = 0; l != 0 && i < cities ; i++)
            {
                NodeMatrix[start, i] = double.MaxValue;
                NodeMatrix[i, end] = double.MaxValue;
            }
            NodeMatrix[end, 0] = double.MaxValue;
            Level = l;
            Vertex = end;

        }
        public LiveNode() { }
        //----------------------------------------------------------------------------------
        public List<double> RowToList(int rowNumber)
        {
            if (NodeMatrix == null)
                throw new EmptyMatrixException(new Exception());
            List<double> list = new List<double>();
            for (int i = 0; i < NodeMatrix.GetLength(0); i++)
            {
                list.Add(NodeMatrix[rowNumber, i]);
            }
            return list;
        }
        public List<double> ColumnToList(int columnNumber)
        {
            if (NodeMatrix == null)
                throw new EmptyMatrixException(new Exception());
            List<double> list = new List<double>();
            for (int i = 0; i < NodeMatrix.GetLength(0); i++)
            {
                list.Add(NodeMatrix[i, columnNumber]);
            }
            return list;
        }

        public double StandrizeRow(int rowNumber) //search for minimum element in row, then subtract it from each other expect infinity
        {
            if (NodeMatrix.GetLength(0) == 0) throw new EmptyMatrixException(new Exception());
            //Search for minimum element
            var minimumElementIndex = MathUtils.GetMinimumElementIndex(RowToList(rowNumber));
            //subtract minimum element from each other
            var minimumElement = NodeMatrix[rowNumber, minimumElementIndex];
            for (int i = 0; i < NodeMatrix.GetLength(0); i++)
            {
                if (NodeMatrix[rowNumber, i] == double.MaxValue) continue;
                NodeMatrix[rowNumber, i] -= minimumElement;
            }
            if (minimumElement != double.MaxValue)
                return minimumElement;
            return 0;
        }

        public double StandarizeColumn(int columnNumber)
        {
            if (NodeMatrix == null) throw new EmptyMatrixException(new Exception());
            //Search for minimum element
            var minimumElementIndex = MathUtils.GetMinimumElementIndex(ColumnToList(columnNumber));
            //subtract minimum element from each other
            var minimumElement = NodeMatrix[minimumElementIndex, columnNumber];
            for (int i = 0; i < NodeMatrix.GetLength(0); i++)
            {
                if (NodeMatrix[i, columnNumber] == double.MaxValue) continue;
                NodeMatrix[i, columnNumber] -= minimumElement;
            }
            if (minimumElement != double.MaxValue)
                return minimumElement;
            return 0;
        }

        public void StandarizeMatrix() //standarize matrix and return LB
        {
            var lb = 0.0;
            for (int i = 0; i < NodeMatrix.GetLength(0); i++)
            {
                lb += StandrizeRow(i);
            }
            for (int j = 0; j < NodeMatrix.GetLength(0); j++)
            {
                lb += StandarizeColumn(j);
            }
            Cost = lb;
        }
    }
}