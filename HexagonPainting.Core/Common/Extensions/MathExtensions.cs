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
}
