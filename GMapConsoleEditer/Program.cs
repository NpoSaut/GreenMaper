using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMapElements;
using System.IO;

namespace GMapConsoleEditer
{
    class Program
    {
        static void Main(string[] args)
        {
            String GMapPath = args[0];

            var map = GMap.Load(new FileStream(GMapPath, FileMode.Open));

            map.Save(new FileStream("export.gps", FileMode.Create));
        }
    }
}
