using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GMapElements
{
    public abstract class GElement
    {
        public int DataLength
        {
            get { return LengthOf(this.GetType()); }
        }

        public static T FromBytes<T>(Byte[] Data) where T : GElement, new()
        {
            var res = new T();
            res.FillWithBytes(Data);
            return res;
        }

        protected abstract void FillWithBytes(Byte[] Data);
        protected virtual void FillFromStream(Stream DataStream)
        {
            var buff = new Byte[DataLength];
            DataStream.Read(buff, 0, buff.Length);
            this.FillWithBytes(buff);
        }

        internal static int LengthOf<T>() where T : GElement
        {
            return LengthOf(typeof(T));
        }
        internal static int LengthOf(Type T)
        {
            return T.GetCustomAttributes(typeof(GLengthAttribute), true).OfType<GLengthAttribute>().First().Length;
        }

        internal static T FromStream<T>(Stream str) where T : GElement, new()
        {
            var res = new T();
            res.FillFromStream(str);
            return res;
        }

        protected static Byte[] ByteSubseq(Byte[] data, int startIndex, int getLength, int echoLength)
        {
            var res = new Byte[echoLength];
            Buffer.BlockCopy(data, startIndex, res, 0, getLength);
            return res;
        }
        protected static int SubInt(Byte[] data, int startIndex, int length)
        {
            return BitConverter.ToInt32(ByteSubseq(data, startIndex, length, 4), 0);
        }
    }
}
