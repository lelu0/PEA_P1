using System;
using System.Collections.Generic;

namespace project1PEA
{
    public class ProblemInstance
    {
        public WorldMap WorldMap { get; set; }
        public List<RouteElement> BestRoute { get; set; }


        public ProblemInstance(WorldMap worldMap)
        {
            WorldMap = worldMap;
            BestRoute = new List<RouteElement>();
        }
        public ProblemInstance(int cities)
        {
            WorldMap = new WorldMap(cities);
            BestRoute = new List<RouteElement>();
        }

        public void RefactorMatrixByMaxElement()
        {
            List<double> rowMinimums = new List<double>();
            List<double> columnMinimums = new List<double>();
            var maxInRow = true;
            var maxIndex = 0;
            double maxValue = 0.0;
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                //Search for minimum element in row and column, then add to list
                var minimumElementIndex = MathUtils.GetMinimumElementIndex(WorldMap.RowToList(i));
                rowMinimums.Add(WorldMap.CityMatrix[i,minimumElementIndex]);
                minimumElementIndex = MathUtils.GetMinimumElementIndex(WorldMap.ColumnToList(i));
                columnMinimums.Add(WorldMap.CityMatrix[minimumElementIndex,i]);
            }

            for (int i = 0; i < WorldMap.Cities; i++)
            {
                if (maxValue < rowMinimums[i])
                {
                    maxValue = rowMinimums[i];
                    maxIndex = i;
                }
            }
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                if (maxValue < columnMinimums[i])
                {
                    maxValue = columnMinimums[i];
                    maxIndex = i;
                    maxInRow = false;
                }
            }

            //MATRIX REFACTOR 
            if (maxInRow)
            {
                int zeroColumnIndex = WorldMap.RowToList(maxIndex).IndexOf(0.0);
                WorldMap.MatrixReduction(maxIndex, zeroColumnIndex);
            }
            else
            {
                var zeroRowIndex = WorldMap.ColumnToList(maxIndex).IndexOf(0.0);
                WorldMap.MatrixReduction(zeroRowIndex,maxIndex);
            }
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
            return WorldMap.CityMatrix[rowNumber, minimumElementIndex];
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
            return WorldMap.CityMatrix[minimumElementIndex, columnNumber];
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