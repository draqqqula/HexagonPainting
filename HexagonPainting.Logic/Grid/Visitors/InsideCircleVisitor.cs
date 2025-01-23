using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Grid;
using HexagonPainting.Core.Grid.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Grid.Visitors;

public class InsideCircleVisitor : IGridVisitor<IEnumerable<GridLocation>>
{
    readonly struct Span
    {
        public required int Start { get; init; }
        public required float Count { get; init; }
    }

    public required Vector2 Position { get; init; }
    public required float Radius { get; init; }
    public IEnumerable<GridLocation> VisitFlatTop(HexagonGridFlatTop grid)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<GridLocation> VisitPointyTop(HexagonGridPointyTop grid)
    {
        var topCornerTile = GetPole(Position.Y, Radius, true, grid.QuarterHeight);
        var bottomCornerTile = GetPole(Position.Y, Radius, false, grid.QuarterHeight);
        var topBoundTile = MathF.Ceiling(Position.Y / grid.QuarterHeight);
        for (var i = topCornerTile + 1; i <= bottomCornerTile; i += 3)
        {
            var isTopHalf = i > topBoundTile;
            var factor = isTopHalf ? -1 : 1;
            var y = i - factor;
            var isBlue = Math.Abs(y % 2) != 0;
            var mapY = Convert.ToInt32(BooleanRound((float)(y - factor) / 3, !isTopHalf)) + factor;

            var side = GetHorizontalSpan(Position, grid.QuarterHeight, grid.HalfWidth, y, Radius, mapY, isBlue);
            var pointy = GetHorizontalSpan(Position, grid.QuarterHeight, grid.HalfWidth, y - factor, Radius, mapY, !isBlue);


            for (var offsetX = 0; offsetX < side.Count && offsetX <= pointy.Count; offsetX++)
            {
                yield return new GridLocation() { Q = mapY, R = Math.Max(side.Start, pointy.Start) + offsetX };
            }
        }
    }

    private static float GetPole(float y, float radius, bool down, float segmentLength)
    {
        var factor = down ? 1 : -1;
        var top = y - radius * factor;
        var tileY = BooleanRound(top / segmentLength, down);
        var topVertexY = BooleanRound((tileY - factor) / 3, down) + factor;
        var topCornerTile = topVertexY * 3 + 2 * factor;
        return topCornerTile;
    }

    private static Span GetHorizontalSpan(Vector2 position, float segmentHeight, float segmentWidth, float i, float radius, float mapY, bool isBlue)
    {
        var sideY = i * segmentHeight - position.Y;
        var sideX = -MathF.Sqrt(MathF.Pow(radius, 2) - MathF.Pow(sideY, 2)) + position.X;
        var sideLeftCorner = MathF.Ceiling((sideX - (isBlue ? segmentWidth : 0)) / (segmentWidth * 2)) * segmentWidth * 2 + (isBlue ? segmentWidth : 0);
        var sideSlice = MathF.Floor(((position.X - sideX) * 2 - sideLeftCorner + sideX) / (segmentWidth * 2));

        var sideMapX = GetSegment(sideLeftCorner, 1 / (segmentWidth * 2), segmentWidth - mapY * segmentWidth);

        return new Span()
        {
            Start = sideMapX,
            Count = sideSlice
        };
    }

    private static int GetSegment(float x, float factor, float offset)
    {
        return Convert.ToInt32(MathF.Floor((x + offset) * factor));
    }

    private static float BooleanRound(float number, bool down)
    {
        return down ? MathF.Floor(number) : MathF.Ceiling(number);
    }
}
