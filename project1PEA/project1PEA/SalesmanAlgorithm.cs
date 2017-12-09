using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    class SalesmanAlgorithm
    {
        /*
         * Class imported from SDIZO Project 3
         */
        public static List<int> Greedy(double[,] cityMatrix, int numberOfCities)
        {
            List<int> cityOrder = new List<int>();
            bool[] cityStatus = new bool[numberOfCities]; //flags for all cities, true - visited;
            for (int c = 0; c < numberOfCities; c++) cityStatus[c] = false; // no city is visited
            int startCity = 0;
            cityOrder.Add(0);
            cityStatus[0] = true; //city 0 is already visited
            int currentCity = 0;
            bool comeback = false; //flag if algorithm is done
            while (!comeback)
            {
                double min = double.MaxValue;
                int minIndex = 0;
                for (int i = 0; i < numberOfCities; i++)
                {
                    if (cityMatrix[currentCity, i] < min && i != currentCity && !cityStatus[i])
                    {
                        min = cityMatrix[currentCity, i];
                        minIndex = i;
                    }
                }
                cityOrder.Add(minIndex);
                currentCity = minIndex;
                cityStatus[minIndex] = true;
                if (currentCity == startCity || cityOrder.Count == numberOfCities)
                {
                    cityOrder.Add(0);
                    comeback = true;
                }

            }
            return cityOrder;
        }
    }
}
