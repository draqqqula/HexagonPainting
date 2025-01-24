using HexagonPainting.Core.Grid.Interfaces;

namespace HexagonPainting.Core.Grid;

public class HexagonGridPointyTop : IGrid
{
    public HexagonGridPointyTop(float inscribedRadius)
    {
        HalfWidth = inscribedRadius;
        CellSide = HalfWidth * 2 / MathF.Sqrt(3);
        HalfHeight = CellSide * 2 - CellSide;
        QuarterHeight = HalfHeight / 2;
        QuarterWidth = HalfWidth / 2;
        Width = HalfWidth * 2;
        Height = HalfHeight * 2;
    }
    public float CellSide { get; private init; }

    public float QuarterWidth { get; private init; }
    public float HalfWidth { get; private init; }
    public float Width { get; private init; }

    public float QuarterHeight { get; private init; }
    public float HalfHeight { get; private init; }
    public float Height { get; private init; }

    public TResult Accept<TResult>(IGridVisitor<TResult> visitor)
    {
        return visitor.VisitPointyTop(this);
    }
}
