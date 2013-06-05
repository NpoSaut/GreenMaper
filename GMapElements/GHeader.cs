using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    [GLength(9)]
    public class GHeader : GElement
    {
        public int PostsCount { get; set; }

        protected override void FillWithBytes(byte[] Data)
        {
            this.PostsCount = BitConverter.ToInt16(Data, 2);
        }
    }
}
