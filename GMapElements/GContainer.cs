using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GMapElements
{
    public abstract class GContainer<TChild> : GElement
        where TChild : GElement, new()
    {
        protected List<TChild> Children { get; set; }
        protected int ChildrenStartAdress { get; set; }
        protected int ChildrenCount { get; set; }
        
        protected virtual int GetChildrenCount(Stream DataStream)
        {
            return ChildrenCount;
        }

        protected override void FillFromStream(Stream DataStream)
        {
            base.FillFromStream(DataStream);
            LoadChildren(DataStream);
        }

        protected void LoadChildren(Stream DataStream)
        {
            if (!DataStream.CanSeek) throw new ArgumentException("Поток данных должен поддерживать функцию поиска", "DataStream");

            var _StartPosition = DataStream.Position;
            Children = new List<TChild>();
            int ChildLength = GElement.LengthOf<TChild>();

            DataStream.Seek(ChildrenStartAdress, SeekOrigin.Begin);
            int cCount = GetChildrenCount(DataStream);

            for (int i = 0; i < cCount; i++)
            {
                TChild child = GElement.FromStream<TChild>(DataStream);
                Children.Add(child);
            }
            DataStream.Seek(_StartPosition, SeekOrigin.Begin);
        }
    }
}
