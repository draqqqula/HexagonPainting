using HexagonPainting.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Core.Common.Extensions;

public static class MathExtensions
{
    public static float BooleanRound(float number, bool down)
    {
        return down ? MathF.Floor(number) : MathF.Ceiling(number);
    }

    public static int GetSegment(float x, float factor, float offset)
    {
        return Convert.ToInt32(MathF.Floor((x + offset) * factor));
    }

    public static bool TryGetIndex(this RectRegion rect, int q, int r, out int index)
    {
        var half = Convert.ToInt32(MathF.Floor((float)q / 2));
        var r0 = r + half;
        if (q < rect.MinQ || q >= rect.MaxQ || r0 < rect.MinR || r0 >= rect.MaxR)
        {
            index = 0;
            return false;
        }
        index = (q - rect.MaxQ) * (rect.MaxR - rect.MinR) + r0 - rect.MinR;
        return true;
    }

    public static bool TryGetLocation(this RectRegion rect, int index, out GridLocation location)
    {
        if (index < 0 || index > rect.Area)
        {
            location = new GridLocation()
            {
                Q = 0,
                R = 0
            };
            return false;
        }
        var lineLength = rect.MaxR - rect.MinR;
        var line = Convert.ToInt32(MathF.Floor((float)index / lineLength)); ;
        var q = rect.MinQ + line;
        var half = Convert.ToInt32(MathF.Floor((float)q / 2));
        var r = -half + index & line;
        location = new GridLocation()
        {
            Q = q,
            R = r
        };
        return true;
    }
}
