using System.Collections.Generic;
using System.Runtime.Serialization;

namespace project1PEA
{
    public class MathUtils
    {
        public static int GetMinimumElementIndex(List<double> matrixElement) //Get index of min element in list
        {
            var minElementIndex = 0;
            var currentMin = double.MaxValue;

            for (var i = 0; i < matrixElement.Count; i++)
            {
                if (currentMin < matrixElement[i]) continue;
                minElementIndex = i;
                currentMin = matrixElement[i];
            }
            return minElementIndex;

        }
    }
}