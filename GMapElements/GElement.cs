using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    public abstract class GElement
    {
        public abstract int Length { get; }

        public static T FromBytes<T>(Byte[] Data) where T : GElement, new()
        {
            var res = new T();
            res.FillWithBytes(Data);
            return res;
        }

        protected abstract void FillWithBytes(Byte[] Data);

        internal static T FromStream<T>(System.IO.Stream str) where T : GElement, new()
        {
            var res = new T();
            var buff = new Byte[res.Length];
            str.Read(buff, 0, buff.Length);
            res.FillWithBytes(buff);
            return res;
        }
    }
}
