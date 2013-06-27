using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GMapElements.Placements
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    abstract class PlacementAttribute : Attribute
    {
        public abstract void DecodeProperty(PropertyInfo P, Object obj, Byte[] buff);
        public abstract void EncodeProperty(PropertyInfo P, Object obj, Byte[] buff);

        public static PlacementAttribute GetPlacementAttribute(PropertyInfo P)
        {
            return (PlacementAttribute)PlacementAttribute.GetCustomAttribute(P, typeof(PlacementAttribute));
        }
    }

    abstract class PlacementAttribute<T> : PlacementAttribute
    {
        public abstract T Get(Byte[] buff);
        public abstract void Set(T value, Byte[] buff);

        public override void DecodeProperty(PropertyInfo P, object obj, byte[] buff)
        {
            P.SetValue(obj, Get(buff), new object[0]);
        }

        public override void EncodeProperty(PropertyInfo P, object obj, byte[] buff)
        {
            Set((T)P.GetValue(obj, new object[0]), buff);
        }
    }
}
