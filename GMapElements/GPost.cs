using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    public enum OrdinateDirection { Increasing = 0, Decreasing = 1 }
    public enum PositionInSection { Middle = 0, Start = 1, End = 2 }

    public class GPost : GElement
    {
        public override int Length
        {
            get { return 15; }
        }

        /// <summary>
        /// Линейная ордината
        /// </summary>
        public int Ordinate { get; set; }
        /// <summary>
        /// Широта и долгота точки
        /// </summary>
        public EarthPoint Point { get; set; }

        internal PositionInSection Position { get; set; }

        protected override void FillWithBytes(byte[] Data)
        {
            this.Ordinate = BitConverter.ToInt32(Data.Take(3).Concat(new Byte[1]).ToArray(), 0);

            byte flags = Data[3];

            this.Position = (PositionInSection)(flags & 0x03);

            this.Point = new EarthPoint(
                BitConverter.ToInt32(Data, 4) * 10e-9 * 180 / Math.PI,
                BitConverter.ToInt32(Data, 8) * 10e-9 * 180 / Math.PI
                );
        }

        public override string ToString()
        {
            return string.Format("{0:N0} : {1}   - {2}", Ordinate, Point, Position);
        }
    }
}
