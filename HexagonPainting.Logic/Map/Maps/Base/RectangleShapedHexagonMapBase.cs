using HexagonPainting.Core.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Map.Maps.Base;

public class RectangleShapedHexagonMapBase : HexagonMapBase
{
    protected readonly RectRegion Rect;
    public RectangleShapedHexagonMapBase(RectRegion rect)
    {
        Rect = rect;
    }

    public override bool TryGetIndex(int q, int r, out int index)
    {
        var half = Convert.ToInt32(MathF.Floor((float)q / 2));
        var r0 = r + half;
        if (q < Rect.MinQ || q >= Rect.MaxQ || r0 < Rect.MinR || r0 >= Rect.MaxR)
        {
            index = 0;
            return false;
        }
        index = (q - Rect.MaxQ) * (Rect.MaxR - Rect.MinR) + r0 - Rect.MinR;
        return true;
    }

    public override bool TryGetLocation(int index, out GridLocation location)
    {
        throw new NotImplementedException();
    }
}
