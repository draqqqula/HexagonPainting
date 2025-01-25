using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Brushes.Base;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Grid.Visitors;

namespace HexagonPainting.Logic.Drawing.Brushes;

public class CircleBrush<TColor> : PointerBrushBase<TColor, InsideCircleVisitor>
{
    public CircleBrush(ISelectedValueProvider<TColor> selectedColor, IGrid grid, IPointer pointer) : base(selectedColor, grid, pointer)
    {
    }

    public float Radius { get; set; } = 15f;

    public override IEnumerable<GridLocation> GetTiles()
    {
        Visitor.Radius = Radius;
        return base.GetTiles();
    }
}
