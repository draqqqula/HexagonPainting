using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Brushes.Base;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Grid.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Brushes;

public class PointBrush<TColor> : PointerBrushBase<TColor, TouchingRectangleVisitor>
{
    public PointBrush(ISelectedValueProvider<TColor> selectedColor, IGrid grid, IPointer pointer) : base(selectedColor, grid, pointer)
    {
    }

    public Vector2 Size { get; set; } = Vector2.One;

    public override IEnumerable<GridLocation> GetTiles()
    {
        Visitor.Size = Size;
        return base.GetTiles();
    }
}
