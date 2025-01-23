namespace HexagonPainting.Core.Grid.Interfaces;

public interface IGrid
{
    public TResult Accept<TResult>(IGridVisitor<TResult> visitor);
}
