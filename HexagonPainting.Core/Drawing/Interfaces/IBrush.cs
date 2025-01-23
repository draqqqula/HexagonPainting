namespace HexagonPainting.Core.Drawing.Interfaces;

public interface IBrush<TColor>
{
    public IFigure<TColor> Draw();
}