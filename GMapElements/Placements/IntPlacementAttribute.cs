using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements.Placements
{
    class IntPlacementAttribute : BytePlacementAttribute<Int32>
    {
        public IntPlacementAttribute(int Offset, int Length)
            : base(Offset, Length)
        {
            this.Encoder = BitConverter.GetBytes;
            this.Decoder = b => BitConverter.ToInt32(b, 0);
        }

        public IntPlacementAttribute(int Offset)
            : base(Offset, 4)
        { }
    }
}
