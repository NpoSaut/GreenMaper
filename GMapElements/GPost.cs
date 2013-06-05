using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    public enum OrdinateDirection { Increasing = 1, Decreasing = -1 }
    public enum PositionInSection { Middle = 0, Start = 1, End = 2 }

    [GLength(15)]
    public class GPost : GContainer<GTrack>
    {
        /// <summary>
        /// Линейная ордината
        /// </summary>
        public int Ordinate { get; set; }
        /// <summary>
        /// Широта и долгота точки
        /// </summary>
        public EarthPoint Point { get; set; }
        public OrdinateDirection Direction { get; set; }

        public IList<GTrack> Tracks
        {
            get { return this.Children.Where(t => t.IsValid).ToList(); }
        }

        internal PositionInSection Position { get; set; }

        protected override void FillWithBytes(byte[] Data)
        {
            this.Ordinate = BitConverter.ToInt32(Data.Take(3).Concat(new Byte[1]).ToArray(), 0);

            byte flags = Data[3];

            this.Direction = DecodeDirection((flags >> 7) & 0x01);
            this.Position = (PositionInSection)(flags & 0x03);

            this.Point = new EarthPoint(
                BitConverter.ToInt32(Data, 4) * 10e-9 * 180 / Math.PI,
                BitConverter.ToInt32(Data, 8) * 10e-9 * 180 / Math.PI
                );

            this.ChildrenStartAdress = SubInt(Data, 12, 3) + 1;
        }
        private OrdinateDirection DecodeDirection(int DirectionCode)
        {
            switch (DirectionCode)
            {
                case 0: return OrdinateDirection.Increasing;
                case 1: return OrdinateDirection.Decreasing;
            }
            throw new ArgumentException("Недопустимое значение кода направления", "DirectionCode");
        }

        protected override int GetChildrenCount(System.IO.Stream DataStream)
        {
            DataStream.Seek(-1, System.IO.SeekOrigin.Current);
            return DataStream.ReadByte();
        }

        public override string ToString()
        {
            return string.Format("{0:N0} : {1}   - {2}", Ordinate, Point, Position);
        }
    }
}
