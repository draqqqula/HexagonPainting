namespace HexagonPainting.Core.Interfaces;

public interface IGrid
{
    public TResult Accept<TResult>(IGridVisitor<TResult> visitor);
}
