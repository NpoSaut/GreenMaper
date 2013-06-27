using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements.Placements
{
    class EnumPlacementAttribute<T> : BitPlacementAttribute<T>
    {
        public EnumPlacementAttribute(int ByteOffset, int BitOffset, int BitLength)
            : base(ByteOffset, BitOffset, BitLength)
        {
        }
    }
}
