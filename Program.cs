using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlMaker
{
    class Program
    {
        private static int _elementIndex = 0;
        private static Random _random = new Random();
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Arg1 is data count");
                Console.ReadLine();
                return;
            }
            //Console.WriteLine(DateTime.Now.ToString("O"));

            var dataCount = int.Parse(args[0]);
            const string file = "data.xml";
            Console.WriteLine(DateTime.Now.ToString("O"));
            WriteXmlFile(dataCount, file);
            Console.WriteLine(DateTime.Now.ToString("O"));
        }

        private static void WriteXmlFile(int dataCount, string file)
        {



            using (var writer = XmlWriter.Create(file))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Root");

                for (var i = 0; i < dataCount; i++)
                {
                    var element = GetData();
                    //foreach (var element in data)
                    {
                        writer.WriteStartElement(element.ElementName);
                        foreach (var attribute in element.Attributes)
                        {
                            writer.WriteAttributeString(attribute.Key, attribute.Value);
                        }

                        foreach (var child in element.ChildNodes)
                        {
                            writer.WriteStartElement(child.ElementName);

                            foreach (var attribute in child.Attributes)
                            {
                                writer.WriteAttributeString(attribute.Key, attribute.Value);
                            }
                            writer.WriteEndElement();
                            if (i % 100000 == 0)
                                Console.WriteLine(i);
                        }

                        writer.WriteEndElement();
                    }
                }


                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        //private static XmlData GetData(int dataCount)
        private static XmlData GetData()
        {
            //var root = new XmlData
            //{
            //    ElementName = "Data",
            //    ChildNodes = new List<XmlData>()
            //};
            //for (var i = 0; i < dataCount; i++)
            //{
            var element = new XmlData
                {
                    ElementName = GetElement(),
                    Attributes = GetAttributes(),
                    ChildNodes = GetChildNodes()
                };
                while (element.Attributes.Count == 0 && element.ChildNodes.Count == 0)
                {
                    element.Attributes = GetAttributes();
                }
                //root.ChildNodes.Add(element);
            //}

            return element;
        }

        private static List<XmlData> GetChildNodes()
        {
            var children = new List<XmlData>();
            var childrenCount = _random.Next(10);
            if (childrenCount < 5) return children;
            childrenCount = childrenCount - 4;
            for (var i = 0; i < childrenCount; i++)
            {
                var child = new XmlData();
                child.ElementName = GetElement(i);
                child.Attributes = GetAttributes();
                children.Add(child);
            }
            return children;
        }

        private static string GetElement()
        {
            _elementIndex++;
            return $"Item{_elementIndex}";
        }

        private static string GetElement(int i)
        {
            return $"Child{i+1}";
        }


        private static Dictionary<string, string> GetAttributes()
        {
            var dict = new Dictionary<string, string>();
            var attributeCount = _random.Next(10);
            for (var i = 0; i < attributeCount; i++)
            {
                dict.Add($"Attribute{i}", _random.Next(200).ToString());
            }

            return dict;
        }
    }
}
