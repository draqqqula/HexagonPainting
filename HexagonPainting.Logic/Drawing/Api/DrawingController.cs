using HexagonPainting.Core.Drawing;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic.Common;

namespace HexagonPainting.Logic.Drawing.Api;

public class DrawingController<TColor>
{
    private readonly ISelectedValueProvider<IBrush<TColor>> _brush;
    private readonly ISelectedValueProvider<IHexagonMap<TColor>> _map;

    public DrawingController(
        ISelectedValueProvider<IBrush<TColor>> brush,
        ISelectedValueProvider<IHexagonMap<TColor>> map)
    {
        _brush = brush;
        _map = map;
    }
    
    public void Draw()
    {
        DrawingHelper.Draw(_map.Value, _brush.Value);
    }
}
