using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements
{
    [GLength(5)]
    public class GTrack : GContainer<GObject>
    {
        public int Number { get; set; }
        internal bool IsValid
        {
            get { return this.ChildrenStartAdress != 0xffffff; }
        }
        public IList<GObject> Objects
        {
            get { return this.Children; }
        }

        protected override void FillWithBytes(byte[] Data)
        {
            this.Number = Data[0];
            this.ChildrenCount = Data[1];
            this.ChildrenStartAdress = SubInt(Data, 2, 3);
        }

        public override string ToString()
        {
            return string.Format("{0} | {1}", Number, string.Join(", ", Objects));
        }
    }
}
