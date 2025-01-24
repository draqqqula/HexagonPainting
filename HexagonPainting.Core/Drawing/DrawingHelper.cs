using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Map.Interfaces;

namespace HexagonPainting.Core.Drawing;

public static class DrawingHelper
{
    public static void Draw<TColor>(IHexagonMap<TColor> map, IBrush<TColor> brush)
    {
        var figure = brush.Draw();
        figure.ApplyTo(map);
    }
}