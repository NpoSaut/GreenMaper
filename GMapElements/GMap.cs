using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GMapElements
{
    public class GMap
    {
        public GHeader Header { get; set; }
        public List<GSection> Sections { get; set; }

        public static GMap Load(Stream FromStream)
        {
            var h = GElement.FromStream<GHeader>(FromStream);

            return new GMap()
            {
                Header = h,
                Sections = LoadSections(FromStream, h.PostsCount).ToList()
            };
        }

        private static IEnumerable<GSection> LoadSections(Stream str, int PostsCount)
        {
            int pc = 0;
            while (pc < PostsCount)
            {
                var sec = new GSection() { Posts = LoadPosts(str).ToList() };
                pc += sec.Posts.Count;
                yield return sec;
            }
        }

        private static IEnumerable<GPost> LoadPosts(Stream str)
        {
            GPost p;
            while(true)
            {
                p = GElement.FromStream<GPost>(str);
                yield return p;
                if (p.Position == PositionInSection.End) yield break;
            }
        }
    }
}
