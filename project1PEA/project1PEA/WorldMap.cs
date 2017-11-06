using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace project1PEA
{
    public class WorldMap
    {
    
        public double[,] CityMatrix { get; set; } //Matrix for cities in world map
        public string Name { get; set; } // Name of the problem instance
        public int Cities { get; set; } //Number of cities in problem instance

        //Constructor take path to the XML File with problem instance
        public WorldMap(string filePath)
        {
            var xmlReader = new XmlTextReader(filePath);
            string lastXmlElement = ""; //Last readed element in XML conversion
            var routesList = new List<double>(); //List of distances from one city to all others. Distance to the start city is 0
            int cityCounter = 0; //Increase by add new city to matrix
            CityMatrix = null; //important cause initialization condition

            while (xmlReader.Read()) //Reading XML file line-by-line
            {
                if (xmlReader.NodeType == XmlNodeType.Element) //getting the name of currently open node
                    lastXmlElement = xmlReader.Name;
                if (xmlReader.NodeType == XmlNodeType.Text && lastXmlElement.Equals("name")) //Use only once to set the name of instance
                {
                    Name = xmlReader.Value;
                    lastXmlElement = "";
                }
                if (xmlReader.NodeType == XmlNodeType.Element && lastXmlElement.Equals("vertex")) //build a new list of distances on every vertex
                {
                    routesList = new List<double>();
                    lastXmlElement = "";
                    cityCounter++;
                }
                if (xmlReader.NodeType == XmlNodeType.Element && lastXmlElement.Equals("edge")) //adding distances from current vertex to others
                {
                    try
                    {
                        if (routesList.Count == cityCounter-1) // if iterator at current v.add 0 (you can't travel to city where you are ;) )
                        {
                            routesList.Add(0);
                        }
                        var tmp = double.Parse(xmlReader.GetAttribute("cost"), NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture); //Converstion from text string in exposition for to double, result in meters
                        routesList.Add(tmp);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name.Equals("vertex")) //List for current vertex is complete, add it to matrix
                {
                    if (CityMatrix == null) //if it's first vertex create a new matrix
                        CityMatrix = new double[routesList.Count, routesList.Count];
                    for (int i = 0; i < routesList.Count; i++)
                        CityMatrix[cityCounter-1, i] = routesList[i];
                }
                if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name.Equals("graph")) //Write total number of cities to property
                    Cities = cityCounter;
            }

        }

        public void printCurrentMap()
        {
            if(CityMatrix == null)
                Console.WriteLine("Your world is empty!");
            else
            {
                for (int i = 0; i < Cities; i++)
                {
                    for (int j = 0; j < Cities; j++)
                    {
                        Console.Write(""+CityMatrix[i,j]+"|");
                    }
                    Console.WriteLine("");
                }
            }
        }
    
    }
}