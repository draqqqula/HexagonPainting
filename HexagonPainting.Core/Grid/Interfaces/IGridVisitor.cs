namespace HexagonPainting.Core.Grid.Interfaces;

public interface IGridVisitor<out TResult>
{
    public TResult VisitPointyTop(HexagonGridPointyTop grid);
    public TResult VisitFlatTop(HexagonGridFlatTop grid);
}
