using System.Collections.Generic;

namespace project1PEA
{
    public class TabuElement
    {
        public List<int> Pair { get; set; }
        public int Cadence { get; set; }

        public TabuElement(List<int> pair, int cadence)
        {
            Pair = pair;
            Cadence = cadence;
        }

        public bool IsEqual(List<int> pair)
        {
            return ((pair[0] == Pair[0] && pair[1] == Pair[1]) || (pair[1] == Pair[0] && pair[0] == Pair[1]));
        }
    }
}