using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    public class GHeader : GElement
    {
        public override int Length
        {
            get { return 9; }
        }

        public int PostsCount { get; set; }

        protected override void FillWithBytes(byte[] Data)
        {
            this.PostsCount = BitConverter.ToInt16(Data, 2);
        }
    }
}
