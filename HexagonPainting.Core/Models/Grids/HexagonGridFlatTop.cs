using HexagonPainting.Core.Interfaces;

namespace HexagonPainting.Core.Models.Grids;

public class HexagonGridFlatTop : IGrid
{
    public TResult Accept<TResult>(IGridVisitor<TResult> visitor)
    {
        return visitor.VisitFlatTop(this);
    }
}
