using System;
using System.Collections.Generic;

namespace project1PEA
{
    public class ProblemInstance
    {
        public WorldMap WorldMap { get; set; }
        public List<int> BestRoute { get; set; }

        public ProblemInstance(WorldMap worldMap)
        {
            WorldMap = worldMap;
            BestRoute = new List<int>();
        }

        public double StandrizeRow(int rowNumber) //search for minimum element in row, then subtract it from each other expect infinity
        {
            
            if (WorldMap.CityMatrix == null) throw new EmptyMatrixException(new Exception());
            //Search for minimum element
            var minimumElementIndex = MathUtils.GetMinimumElementIndex(WorldMap.RowToList(rowNumber));
            //subtract minimum element from each other
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                if(WorldMap.CityMatrix[rowNumber,i] == double.MaxValue) continue;
                WorldMap.CityMatrix[rowNumber, i] -= WorldMap.CityMatrix[rowNumber,minimumElementIndex];
            }
            return WorldMap.CityMatrix[rowNumber, minimumElementIndex];
        }

        public double StandarizeColumn(int columnNumber)
        {
            if (WorldMap.CityMatrix == null) throw new EmptyMatrixException(new Exception());
            //Search for minimum element
            var minimumElementIndex = MathUtils.GetMinimumElementIndex(WorldMap.ColumnToList(columnNumber));
            //subtract minimum element from each other
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                if (WorldMap.CityMatrix[i, columnNumber] == double.MaxValue) continue;
                WorldMap.CityMatrix[i, columnNumber] -= WorldMap.CityMatrix[minimumElementIndex, columnNumber];
            }
            return WorldMap.CityMatrix[minimumElementIndex, columnNumber];
        }

        
    }
}