using HexagonPainting.Core.Interfaces;

namespace HexagonPainting.Core.Models.Grids;

public class HexagonGridPointyTop : IGrid
{
    public HexagonGridPointyTop(float inscribedRadius)
    {
        HalfWidth = inscribedRadius;
        CellSide = HalfWidth * 2 / MathF.Sqrt(3);
        QuarterHeight = (CellSide * 2 - CellSide) / 2;
    }
    public float HalfWidth { get; private init; }
    public float CellSide { get; private init; }
    public float QuarterHeight { get; private init; }

    public TResult Accept<TResult>(IGridVisitor<TResult> visitor)
    {
        return visitor.VisitPointyTop(this);
    }
}
