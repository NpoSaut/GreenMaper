using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GMapElements
{
    enum DeferredWritePriority : int { AfterHeader = 0, AfterPosts = 1, AfterTracks = 2, AfterObjects = 3 }

    class Deferr : IDisposable
    {
        class DeferredBlock
        {
            public long LinkPosition { get; private set; }
            public Stream str { get; private set; }
            public IEnumerable<GElement> Elements { get; private set; }

            public DeferredBlock(Stream str, int LinkOffset, IEnumerable<GElement> Elements)
            {
                LinkPosition = str.Position + LinkOffset;
                this.str = str;
                this.Elements = Elements;
            }

            public void ExecuteWrite()
            {
                long _position = str.Position;
                str.Seek(LinkPosition, SeekOrigin.Begin);
                str.Write(BitConverter.GetBytes(_position), 0, 3);
                str.Seek(_position, SeekOrigin.Begin);
                foreach (var ge in Elements)
                    ge.WriteTo(str);
            }
        }

        private static Dictionary<Stream, Deferr> Roots = new Dictionary<Stream, Deferr>();
        private Stream str { get; set; }
        public Deferr(Stream str)
        {
            this.str = str;
        }

        private Deferr Owner { get; set; }
        private List<DeferredBlock> Blocks = new List<DeferredBlock>();
        public void DeferredWrite(IEnumerable<GElement> Elements)
        {
            DeferredWrite(0, Elements);
        }
        public void DeferredWrite(int LinkOffset, IEnumerable<GElement> Elements)
        {
            var dfb = new DeferredBlock(str, LinkOffset, Elements);
            Blocks.Add(dfb);
        }

        public void Dispose()
        {
            foreach (var d in Blocks)
            {
                d.ExecuteWrite();
            }
        }
    }
}
