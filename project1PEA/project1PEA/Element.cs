using System.Collections.Generic;
using System.Security.Policy;

namespace project1PEA
{
    public class Element
    {
        public int Vertex { get; set; }
        public List<int> VertexSet { get; set; }
        public double Cost { get; set; }
        public int Parent { get; set; }
        public Element(int v, List<int> s)
        {
            Vertex = v;
            VertexSet = s;
        }
        public Element(int v, List<int> s, double cost, int parent)
        {
            Vertex = v;
            VertexSet = s;
            Cost = cost;
            Parent = parent;
        }

        public bool IsEqual(int v, List<int> vs)
        {
            if (v == Vertex && vs.Equals(VertexSet)) return true;
            return false;
        }
    }
}