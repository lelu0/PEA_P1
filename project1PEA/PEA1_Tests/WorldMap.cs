using System;

namespace PEA1_Tests
{
    class WorldMap
    {
        public double[,] CityMatrix { get; set; }
        public int Cities { get; set; }

        public WorldMap(int cities)
        {
            Cities = cities;
            CityMatrix = new double[cities,cities];
            for (int i = 0; i < cities; i++)
            {
                for (int j = 0; j < cities; j++)
                {
                    CityMatrix[i, j] = i + j;
                }
            }
        }
        
        public void MatrixReduction(int row, int column)
        {
            var tmpMatrix = new double[Cities - 1, Cities - 1];
            for (int i = 0; i < Cities; i++)
            {
                if (i == row) continue;
                for (int j = 0; j < Cities; j++)
                {
                    if (j == column) continue;
                    if (j > column)
                        if (i > row) tmpMatrix[i - 1, j - 1] = CityMatrix[i, j]; else tmpMatrix[i, j - 1] = CityMatrix[i, j];
                    else
                    if (i > row) tmpMatrix[i - 1, j] = CityMatrix[i, j]; else tmpMatrix[i, j] = CityMatrix[i, j];
                }
            }
            CityMatrix = tmpMatrix;
            Cities--;
        }

        public void printCurrentMap()
        {
            if (CityMatrix == null)
                Console.WriteLine("Your world is empty!");
            else
            {
                for (int i = 0; i < Cities; i++)
                {
                    for (int j = 0; j < Cities; j++)
                    {
                        Console.Write("" + CityMatrix[i, j] + "|");
                    }
                    Console.WriteLine("");
                }
            }
        }

    }
}