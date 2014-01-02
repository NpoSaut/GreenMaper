using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    /// <summary>
    /// Точка на поверхности Земли
    /// </summary>
    public class EarthPoint
    {
        /// <summary>
        /// Широта
        /// </summary>
        /// <remarks>По Y</remarks>
        public Double Latitude { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        /// <remarks>По X</remarks>
        public Double Longitude { get; set; }



        #region Перевод в радианы
        /// <summary>
        /// Широта (в радианах)
        /// </summary>
        /// <remarks>По Y</remarks>
        public Double LatitudeRad
        {
            get { return Latitude * Math.PI / 180.0; }
            set { Latitude = value * 180.0 / Math.PI; }
        }
        /// <summary>
        /// Долгота (в радианах)
        /// </summary>
        /// <remarks>По X</remarks>
        public Double LongitudeRad
        {
            get { return Longitude * Math.PI / 180.0; }
            set { Longitude = value * 180.0 / Math.PI; }
        }
        #endregion



        /// <summary>
        /// Создаёт точку с указанной широтой и долготой
        /// </summary>
        /// <param name="Latitude">Широта (Y)</param>
        /// <param name="Longitude">Долгота (X)</param>
        public EarthPoint(Double Latitude, Double Longitude)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }

        public override string ToString()
        {
            return string.Format("{0:F4}   {1:F4}", Longitude, Latitude);
        }
    }
}
