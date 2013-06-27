using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class GChildrenLinkAttribute : Attribute
    {
        public int ChildrenLinkOffset { get; private set; }
        public int LinkLength { get; set; }

        public GChildrenLinkAttribute(int ChildrenLinkOffset)
        {
            this.ChildrenLinkOffset = ChildrenLinkOffset;
            this.LinkLength = 3;
        }

        public static GChildrenLinkAttribute Get(Type t)
        {
            return t.GetCustomAttributes(typeof(GChildrenLinkAttribute), false).OfType<GChildrenLinkAttribute>().First();
        }
    }
}
