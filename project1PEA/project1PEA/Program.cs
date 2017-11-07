using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1PEA
{
    class Program
    {
        static void Main(string[] args)
        {
            //WorldMap wm = new WorldMap(@"C:\berlin52x\berlin52.xml");
            // wm.printCurrentMap();
            ProblemInstance problem = new ProblemInstance(4);
            problem.WorldMap.printCurrentMap();
            Console.WriteLine();
            problem.LB = problem.StandarizeMatrix();
            problem.WorldMap.printCurrentMap();
            Console.WriteLine(problem.LB);
            problem.RefactorMatrixByMaxElement();
            problem.WorldMap.printCurrentMap();
            Console.WriteLine("STEP 2");

            //step 2
            problem.StandarizeMatrix();
            problem.WorldMap.printCurrentMap();
            Console.WriteLine(problem.LB);
            problem.RefactorMatrixByMaxElement();
            problem.WorldMap.printCurrentMap();
        }
    }
}
