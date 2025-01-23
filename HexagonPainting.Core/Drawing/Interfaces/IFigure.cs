using HexagonPainting.Core.Map.Interfaces;

namespace HexagonPainting.Core.Drawing.Interfaces;

public interface IFigure<TColor>
{
    public void ApplyTo(IHexagonMap<TColor> map);
}
