﻿using System;
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
            problem.StandarizeMatrix();
            problem.WorldMap.printCurrentMap();
            Console.WriteLine();
            problem.RefactorMatrixByMaxElement();
            problem.WorldMap.printCurrentMap();

        }
    }
}
