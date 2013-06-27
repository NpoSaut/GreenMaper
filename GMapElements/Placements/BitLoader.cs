using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMapElements.Placements
{
    static class BitLoader
    {
        public static void Decode(Object obj, Byte[] buff)
        {
            var t = obj.GetType();
            var BitProperties = t.GetProperties()
                                 .Select(pi => new { pi, a = PlacementAttribute.GetPlacementAttribute(pi) })
                                 .Where(bp => bp.a != null);
            foreach (var bp in BitProperties)
            {
                bp.a.DecodeProperty(bp.pi, obj, buff);
            }
        }
        public static void Encode(Object obj, Byte[] buff)
        {
            var t = obj.GetType();
            var BitProperties = t.GetProperties()
                                 .Select(pi => new { pi, a = PlacementAttribute.GetPlacementAttribute(pi) })
                                 .Where(bp => bp.a != null);
            foreach (var bp in BitProperties)
            {
                bp.a.EncodeProperty(bp.pi, obj, buff);
            }
        }
    }
}
