using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Map.Interfaces;

namespace HexagonPainting.Logic.Drawing.Figure;

public class MonoColorFigure<T> : IFigure<T>
{
    public required T Color { get; init; }
    public required IEnumerable<GridLocation> Region { get; init; }
    public void ApplyTo(IHexagonMap<T> map)
    {
        foreach (GridLocation location in Region)
        {
            map[location] = Color;
        }
    }
}
