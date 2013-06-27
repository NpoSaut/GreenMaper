using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements.Placements
{
    class BytePlacementAttribute<T> : PlacementAttribute<T>
    {
        public int Offset { get; private set; }
        public int Length { get; private set; }
        
        public Converter<T, Byte[]> Encoder { get; set; }
        public Converter<Byte[], T> Decoder { get; set; }

        public BytePlacementAttribute(int Offset, int Length)
        {
            this.Offset = Offset;
            this.Length = Length;
        }

        public override T Get(Byte[] buff)
        {
            var ValueBuffer = new Byte[Length];
            Buffer.BlockCopy(buff, Offset, ValueBuffer, 0, Length);
            return Decoder(ValueBuffer);
        }
        public override void Set(T value, Byte[] buff)
        {
            Buffer.BlockCopy(Encoder(value), 0, buff, Offset, Length);
        }
    }
}
