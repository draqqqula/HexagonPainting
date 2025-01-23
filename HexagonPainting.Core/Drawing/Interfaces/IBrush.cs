namespace HexagonPainting.Core.Drawing.Interfaces;

public interface IBrush<T>
{
    public IFigure<T> Draw();
}