using HexagonPainting.Core.Drawing.Interfaces;

namespace HexagonPainting.Logic.Drawing.Interfaces;

public interface ISelectedBrush<T>
{
    public IBrush<T> Value { get; }
}
