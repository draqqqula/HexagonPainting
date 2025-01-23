using HexagonPainting.Core.Models.Grids;

namespace HexagonPainting.Core.Interfaces;

public interface IGridVisitor<out TResult>
{
    public TResult VisitPointyTop(HexagonGridPointyTop grid);
    public TResult VisitFlatTop(HexagonGridFlatTop grid);
}
