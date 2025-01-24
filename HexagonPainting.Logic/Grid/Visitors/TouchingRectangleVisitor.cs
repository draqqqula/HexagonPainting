using HexagonPainting.Core.Common.Extensions;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Grid;
using HexagonPainting.Core.Grid.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Grid.Visitors;

public class TouchingRectangleVisitor : PositionVisitorBase<IEnumerable<GridLocation>>
{
    public Vector2 Size { get; set; }

    public override IEnumerable<GridLocation> VisitFlatTop(HexagonGridFlatTop grid)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<GridLocation> VisitPointyTop(HexagonGridPointyTop grid)
    {
        var top = Convert.ToInt32(MathExtensions.BooleanRound((Position.Y - grid.QuarterHeight * 2) / (grid.QuarterHeight * 3), false));
        var bottom = Convert.ToInt32(MathExtensions.BooleanRound((Position.Y + Size.Y + grid.QuarterHeight * 2) / (grid.QuarterHeight * 3), true));

        for (var i = top; i <= bottom; i++)
        {
            var isBlue = Math.Abs(i % 2) != 0 ? 0 : -1;
            var left = Convert.ToInt32(MathExtensions.BooleanRound((Position.X - isBlue * grid.HalfWidth) / (grid.HalfWidth * 2), true));
            var right = Convert.ToInt32(MathExtensions.BooleanRound((Position.X + Size.X - isBlue * grid.HalfWidth) / (grid.HalfWidth * 2), true));
            for (var j = left; j <= right; j++)
            {
                yield return new GridLocation()
                {
                    Q = i,
                    R = j - i / 2
                };
            }
        }
    }
}
