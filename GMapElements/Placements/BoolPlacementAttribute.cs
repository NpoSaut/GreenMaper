using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements.Placements
{
    class BoolPlacementAttribute : BitPlacementAttribute<Boolean>
    {
        public BoolPlacementAttribute(int ByteOffset, int BitOffset)
            : base(ByteOffset, BitOffset, 1)
        {
            Decoder = b => b != 0;
            Encoder = b => (byte)(b ? 1 : 0);
        }
    }
}
