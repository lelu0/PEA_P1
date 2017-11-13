using System.Collections.Generic;
using System.Runtime.Serialization;

namespace project1PEA
{
    public class MathUtils
    {
        public static int GetMinimumElementIndex(List<double> matrixElement, bool ignoreFirstZero = false) //Get index of min element in list
        {
            var minElementIndex = 0;
            var currentMin = double.MaxValue;
            for (var i = 0; i < matrixElement.Count; i++)
            {
                if (matrixElement[i] == 0.0 && ignoreFirstZero) //first zero in search is ignored
                {
                    ignoreFirstZero = false;
                    continue;
                }
                if (currentMin <= matrixElement[i]) continue;
                minElementIndex = i;
                currentMin = matrixElement[i];
            }
            return minElementIndex;

        }

        public static LiveNode GetMinCostNode(List<LiveNode> liveNodes)
        {
            LiveNode min = new LiveNode();
            min.Cost = double.MaxValue;
            foreach (var liveNode in liveNodes)
            {
                if (liveNode.Cost < min.Cost)
                    min = liveNode;
            }
            return min;
        }
        public static int GetMinCostNodeIndex(List<LiveNode> liveNodes)
        {
            double min = double.MaxValue;
            int minIndex = 0;
            for (int i = 0; i < liveNodes.Count; i++)
            {

                if (liveNodes[i].Cost < min)
                {
                    min = liveNodes[i].Cost;
                    minIndex = i;
                }
                        
                
            }
            return minIndex;
        }
    }
}