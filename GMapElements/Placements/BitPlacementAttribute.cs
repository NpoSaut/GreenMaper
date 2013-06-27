using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements.Placements
{
    class BitPlacementAttribute<T> : PlacementAttribute<T>
    {
        public int ByteOffset { get; private set; }
        public int BitOffset { get; private set; }
        public int BitLength { get; private set; }

        private int Mask { get; set; }

        public Converter<T, Byte> Encoder { get; set; }
        public Converter<Byte, T> Decoder { get; set; }

        public BitPlacementAttribute(int ByteOffset, int BitOffset, int BitLength)
        {
            this.ByteOffset = ByteOffset;
            this.BitOffset = BitOffset;
            this.BitLength = BitLength;

            Mask = (1 << BitLength - 1) << BitOffset;
        }

        public override T Get(Byte[] buff)
        {
            int val = (buff[ByteOffset]) & Mask >> BitOffset;
            return Decoder((byte)val);
        }
        public override void Set(T value, Byte[] buff)
        {
            int val = buff[ByteOffset];
            val &= ~Mask;
            val |= Encoder(value) << BitOffset;
            buff[ByteOffset] = (byte)val;
        }
    }
}
