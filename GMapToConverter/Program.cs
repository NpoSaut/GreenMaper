using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMapElements;
using System.IO;
using System.Xml.Linq;
using System.Globalization;

namespace GMapToConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            String GMapPath = args[0];
            String KMapPath = args[1];

            var map = GMap.Load(new FileStream(GMapPath, FileMode.Open));

            foreach (var sec in map.Sections)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-----------------------");
                Console.ResetColor();
                foreach (var post in sec.Posts)
                    Console.WriteLine(post);
            }


            var KMap = new XDocument(
                    new XElement(XName.Get("kml", "http://www.opengis.net/kml/2.2"),
                        new XElement("Document",
                            new XElement("name", "Green Field"),
                            map.Sections.Select(sec => GetRandomStyle(sec)),
                            map.Sections.Select(sec =>
                                new XElement("Placemark",
                                    new XElement("name", "line" + GetStyleName(sec)),
                                    new XElement("styleUrl", "#" + GetStyleName(sec)),
                                    new XElement("LineString",
                                        new XElement("coordinates",
                                            string.Join(Environment.NewLine,
                                                sec.Posts.Select(p => string.Format(CultureInfo.InvariantCulture.NumberFormat,
                                                                                    "{0},{1},0",
                                                                                    p.Point.Longitude, p.Point.Latitude))))))),
                            map.Sections.SelectMany(sec =>
                                sec.Posts.Select(p =>
                                new XElement("Placemark",
                                    new XElement("name", string.Format("{0} км", p.Ordinate / 1000.0)),
                                    new XElement("description", string.Format("{0:N0} м", p.Ordinate)),
                                    new XElement("styleUrl", "#" + GetStyleName(sec)),
                                    new XElement("Point",
                                        new XElement("coordinates", string.Format(CultureInfo.InvariantCulture.NumberFormat,
                                                                                    "{0},{1},0",
                                                                                    p.Point.Longitude, p.Point.Latitude))))))
                        )));
            KMap.Save(KMapPath);

            Console.ReadLine();
        }

        private static String GetStyleName(GSection sec)
        {
            return string.Format("section{0}", sec.GetHashCode().ToString());
        }

        private static List<string> colors = new List<string>()
        {
            "ff000",
            "ff6633",
            "ff6699",
            "cc66ff",
            "6699ff",
            "66cc99",
            "99ff00"
        };
        private static int idx = 0; 

        private static XElement GetRandomStyle(GSection sec)
        {
            return new XElement("Style",
                new XAttribute("id", GetStyleName(sec)),
                new XElement("LineStyle",
                    new XElement("color", "ff" + colors[idx = (idx + 1) % colors.Count] ),
                    new XElement("width", 3)));
        }
    }
}
