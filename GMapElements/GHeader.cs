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
            this.PostsCount = BitConverter.ToUInt16(Data, 2);
        }

        protected override byte[] GetBytes()
        {
            var buff = new Byte[DataLength];
            Buffer.BlockCopy(BitConverter.GetBytes((UInt16)PostsCount), 0, buff, 2, 2);
            return buff;
        }
    }
}
