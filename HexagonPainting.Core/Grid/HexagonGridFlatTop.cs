using HexagonPainting.Core.Grid.Interfaces;

namespace HexagonPainting.Core.Grid;

public class HexagonGridFlatTop : IGrid
{
    public TResult Accept<TResult>(IGridVisitor<TResult> visitor)
    {
        return visitor.VisitFlatTop(this);
    }
}