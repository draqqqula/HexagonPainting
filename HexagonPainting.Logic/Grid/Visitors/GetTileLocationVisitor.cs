using HexagonPainting.Core.Common.Extensions;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Grid;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Grid.Visitors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Grid.Visitors;

public class GetTileLocationVisitor : PositionVisitorBase<GridLocation>
{
    public override GridLocation VisitFlatTop(HexagonGridFlatTop grid)
    {
        throw new NotImplementedException();
    }

    public override GridLocation VisitPointyTop(HexagonGridPointyTop grid)
    {
        var virtualX = Position.X;
        var virtualY = Position.Y;

        var tileY = MathExtensions.GetSegment(virtualY, 1 / grid.QuarterHeight, 0);
        var tileX = MathExtensions.GetSegment(virtualX, 1 / grid.HalfWidth, 0);

        var isOrange = (tileY - 1) % 3 == 0;

        var segmentY = Convert.ToInt32(MathF.Floor((float)(tileY - 1) / 3)) + 1;

        if (isOrange && IsOnTriangle(virtualX, virtualY, tileX, tileY, grid.HalfWidth, grid.QuarterHeight))
        {
            segmentY -= 1;
        }

        var segmentX = MathExtensions.GetSegment(virtualX, 1 / (grid.HalfWidth * 2), grid.HalfWidth - segmentY * grid.HalfWidth);

        return new GridLocation()
        {
            Q = segmentY,
            R = segmentX
        };
    }

    private static bool IsOnTriangle(float x, float y, float tileX, float tileY, float segmentWidth, float segmentHeight)
    {
        var x0 = tileX * segmentWidth + segmentWidth / 2;
        var y0 = tileY * segmentHeight + segmentHeight / 2;
        var x1 = (x - x0) / (segmentWidth / 2);
        var y1 = (y - y0) / (segmentHeight / 2);

        var isBlue = Math.Abs((tileY - 1) % 6) <= 2;

        if (isBlue != (tileX % 2 == 0))
        {
            return x1 > y1;
        }
        else
        {
            return x1 < -y1;
        }
    }
}
