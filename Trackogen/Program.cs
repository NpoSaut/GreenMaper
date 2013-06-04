using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using GMapElements;
using System.IO;

namespace Trackogen
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = new EarthPoint(77.1539, -139.398);
            var p2 = new EarthPoint(-77.1804, -139.55);

            var r = p1.DistanceTo(p2);
            

            String GpxPath = args[0];
            String TxtPath = args[1];

            var gpx = XDocument.Load(GpxPath).Root;
            var EarthPoints =
                gpx.Element("trk").Element("trkseg")
                    .Elements("trkpt")
                    .Select(XPoint =>
                        new EarthPoint((Double)XPoint.Attribute("lat"), (Double)XPoint.Attribute("lon")))
                    .ToList();

            using (TextWriter tw = new StreamWriter(TxtPath))
            {
                EarthPoint prewPoint = null;
                double x = 0;
                foreach (var p in EarthPoints)
                {
                    if (prewPoint != null)
                        x += p.DistanceTo(prewPoint);
                    prewPoint = p;

                    var s = string.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat,
                                          "{{ {0:F6}, {1:F6}, {2:F0} }},", p.Latitude, p.Longitude, x);
                    tw.WriteLine(s);
                    Console.WriteLine(s);
                }
            }

            Console.ReadLine();
        }
    }
}
