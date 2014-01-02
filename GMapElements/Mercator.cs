using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace GMapElements
{
    public static class Mercator
    {
        /// <summary>
        /// Радиус Земли в метрах
        /// </summary>
        public const double c = 6371000;
        //public const double ε = 1;

        public static Point GetPoint(this EarthPoint ep)
        {
            return new Point()
            {
                X = c * ep.LongitudeRad,
                Y = c * Math.Log(0.5 * ep.LatitudeRad + Math.PI / 4)
            };
        }

        /// <summary>
        /// Возвращает расстояние между точками по теореме гаверсинусов
        /// </summary>
        /// <param name="p1">Первая точка</param>
        /// <param name="p2">Вторая точка</param>
        /// <returns>Расстояние между точками в метрах</returns>
        public static Double DistanceTo(this EarthPoint p1, EarthPoint p2)
        {
            return 2 * c * Math.Asin(Math.Sqrt(EstimateDistances(p1, p2)));
        }

        public static Double EstimateDistances(this EarthPoint p1, EarthPoint p2)
        {
            return Math.Pow(Math.Sin((p2.LatitudeRad - p1.LatitudeRad) / 2), 2)
                + Math.Cos(p1.LatitudeRad) * Math.Cos(p2.LatitudeRad) * Math.Pow(Math.Sin((p2.LongitudeRad - p1.LongitudeRad) / 2), 2);
        }
    }
}
