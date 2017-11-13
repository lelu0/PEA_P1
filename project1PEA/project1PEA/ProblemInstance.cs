using System;
using System.Collections.Generic;

namespace project1PEA
{
    public class ProblemInstance
    {
        public WorldMap WorldMap { get; set; }
        public List<RouteElement> BestRoute { get; set; }
        public double LB { get; set; }
        public List<int> RowList { get; set; }
        public List<int> ColumnList { get; set; }

        public ProblemInstance(WorldMap worldMap)
        {
            WorldMap = worldMap;
            BestRoute = new List<RouteElement>();
            RowList = new List<int>(WorldMap.Cities);
            ColumnList = new List<int>(WorldMap.Cities);
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                RowList.Add(i);
                ColumnList.Add(i);
            }
        }
        public ProblemInstance(int cities)
        {
            WorldMap = new WorldMap(cities);
            BestRoute = new List<RouteElement>();
            RowList = new List<int>(cities);
            ColumnList = new List<int>(cities);
            for (int i = 0; i < cities; i++)
            {
                RowList.Add(i);
                ColumnList.Add(i);
            }
        }

        public void Solve()
        {
            List<LiveNode> liveNodes = new List<LiveNode>();
            List<int[]> path = new List<int[]>();
            
            //creating of root node 
            //TODO reduction 
        }

        public double StandrizeRow(int rowNumber) //search for minimum element in row, then subtract it from each other expect infinity
        {
            
            if (WorldMap.CityMatrix == null) throw new EmptyMatrixException(new Exception());
            //Search for minimum element
            var minimumElementIndex = MathUtils.GetMinimumElementIndex(WorldMap.RowToList(rowNumber));
            //subtract minimum element from each other
            var minimumElement = WorldMap.CityMatrix[rowNumber, minimumElementIndex];
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                if(WorldMap.CityMatrix[rowNumber,i] == double.MaxValue) continue;
                WorldMap.CityMatrix[rowNumber, i] -= minimumElement;
            }
            return minimumElement;
        }

        public double StandarizeColumn(int columnNumber)
        {
            if (WorldMap.CityMatrix == null) throw new EmptyMatrixException(new Exception());
            //Search for minimum element
            var minimumElementIndex = MathUtils.GetMinimumElementIndex(WorldMap.ColumnToList(columnNumber));
            //subtract minimum element from each other
            var minimumElement = WorldMap.CityMatrix[minimumElementIndex, columnNumber];
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                if (WorldMap.CityMatrix[i, columnNumber] == double.MaxValue) continue;
                WorldMap.CityMatrix[i, columnNumber] -= minimumElement;
            }
            return minimumElement;
        }

        public double StandarizeMatrix() //standarize matrix and return LB
        {
            var lb = 0.0;
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                lb += StandrizeRow(i);
            }
            for (int j = 0; j < WorldMap.Cities; j++)
            {
                lb += StandarizeColumn(j);
            }
            return lb;
        }

        
        
    }
}