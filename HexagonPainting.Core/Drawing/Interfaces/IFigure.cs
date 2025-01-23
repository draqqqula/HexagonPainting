using HexagonPainting.Core.Map.Interfaces;

namespace HexagonPainting.Core.Drawing.Interfaces;

public interface IFigure<T>
{
    public void ApplyTo(IHexagonMap<T> map);
}
