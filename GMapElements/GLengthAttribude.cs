using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    class GLengthAttribute : Attribute
    {
        public int Length { get; private set; }

        public GLengthAttribute(int Length)
        {
            this.Length = Length;
        }
    }
}
