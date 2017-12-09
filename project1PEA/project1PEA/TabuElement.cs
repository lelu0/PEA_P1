namespace project1PEA
{
    internal class TabuElement
    {
        public int Index { get; set; }
        public int Cadence { get; set; }

        public TabuElement(int index, int cadence)
        {
            Index = index;
            Cadence = cadence;
        }
    }
}