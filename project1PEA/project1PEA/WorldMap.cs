using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

class WorldMap
{
    public double[,] CityMatrix { get; set; }
    public string Name { get; set; }
    public WorldMap(string filePath)
    {
        var xmlReader = new XmlTextReader(filePath);
        string lastXmlElement = "";
        var routesList = new List<double>();
        var iterator = 0;
        int cityCounter = 0;
        CityMatrix = null;
        while (xmlReader.Read())
        {
            if (xmlReader.NodeType == XmlNodeType.Element)
                lastXmlElement = xmlReader.Name;
            if (xmlReader.NodeType == XmlNodeType.Text && lastXmlElement.Equals("name"))
            {
                Name = xmlReader.Value;
                lastXmlElement = "";
            }
            if (xmlReader.NodeType == XmlNodeType.Element && lastXmlElement.Equals("vertex"))
            {
                routesList = new List<double>();
                lastXmlElement = "";
                cityCounter++;
            }
            if (xmlReader.NodeType == XmlNodeType.Element && lastXmlElement.Equals("edge"))
            {
                double tmp;
                try
                {

                    if (iterator == cityCounter-1)
                    {
                        routesList.Add(-1);
                        iterator++;
                    }
                    tmp = double.Parse(xmlReader.GetAttribute("cost"), NumberStyles.AllowExponent | NumberStyles.Float, CultureInfo.InvariantCulture);
                    routesList.Add(tmp);
                    iterator++;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name.Equals("vertex"))
            {
                iterator = 0;
                if (CityMatrix == null)
                    CityMatrix = new double[routesList.Count, routesList.Count];
                for (int i = 0; i < routesList.Count; i++)
                    CityMatrix[cityCounter-1, i] = routesList[i];
            }

        }

    }

    
}