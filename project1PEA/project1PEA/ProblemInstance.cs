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

        public void RefactorMatrixByMaxElement()
        {
            List<double> rowMinimums = new List<double>();
            List<double> columnMinimums = new List<double>();
            var maxInRow = true;
            var maxIndex = 0;
            double maxValue = 0.0;
            for (int i = 0; i < WorldMap.Cities; i++)
            {
                //Search for minimum element in row and column, then add to list. First zero is ignored
                var minimumElementIndex = MathUtils.GetMinimumElementIndex(WorldMap.RowToList(i),true);
                rowMinimums.Add(WorldMap.CityMatrix[i,minimumElementIndex]);
                minimumElementIndex = MathUtils.GetMinimumElementIndex(WorldMap.ColumnToList(i),true);
                columnMinimums.Add(WorldMap.CityMatrix[minimumElementIndex,i]);
            }

            for (int i = 0; i < WorldMap.Cities; i++)// Search for maximum value in column
            {
                if (maxValue < columnMinimums[i])
                {
                    maxValue = columnMinimums[i];
                    maxIndex = i;
                    maxInRow = false;
                }
            }
            for (int i = 0; i < WorldMap.Cities; i++) //search for max value in row
            {
                if (maxValue < rowMinimums[i])
                {
                    maxValue = rowMinimums[i];
                    maxIndex = i;
                }
            }
            

            //MATRIX REFACTOR 
            if (maxInRow)
            {
                int zeroColumnIndex = WorldMap.RowToList(maxIndex).IndexOf(0.0);
                WorldMap.MatrixReduction(maxIndex, zeroColumnIndex);
                BestRoute.Add(new RouteElement(RowList[maxIndex],ColumnList[zeroColumnIndex])); //add new step on road
                LB += WorldMap.CityMatrix[zeroColumnIndex, maxIndex];
                WorldMap.CityMatrix[zeroColumnIndex,maxIndex] = double.MaxValue; //Block way back
                RowList.RemoveAt(maxIndex); //Remove from List
                ColumnList.RemoveAt(zeroColumnIndex);
            }
            else
            {
                var zeroRowIndex = WorldMap.ColumnToList(maxIndex).IndexOf(0.0); //TODO Debug row/column list concept
                WorldMap.MatrixReduction(zeroRowIndex,maxIndex); 
                BestRoute.Add(new RouteElement(RowList[zeroRowIndex], ColumnList[maxIndex])); //add new step on road
                LB += WorldMap.CityMatrix[maxIndex, zeroRowIndex];
                WorldMap.CityMatrix[maxIndex,zeroRowIndex] = double.MaxValue; //Block way back
                RowList.RemoveAt(zeroRowIndex); //Remove from List
                ColumnList.RemoveAt(maxIndex);
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