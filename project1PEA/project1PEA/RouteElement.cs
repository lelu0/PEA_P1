using System.ComponentModel;

namespace project1PEA
{
    public class RouteElement
    {
        public int Start { get; set; }
        public int End { get; set; }

        public RouteElement()
        {
            
        }

        public RouteElement(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}