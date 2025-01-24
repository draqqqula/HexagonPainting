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

public class PointBrush<TColor> : MonoColorBrushBase<TColor>
{
    private readonly IPointer _pointer;
    private readonly TouchingRectangleVisitor _visitor;
    public PointBrush(ISelectedValueProvider<TColor> selectedColor, IGrid grid, IPointer pointer) : base(selectedColor, grid)
    {
        _pointer = pointer;
        _visitor = new TouchingRectangleVisitor();
    }

    public Vector2 Size { get; set; } = Vector2.One;

    public override IEnumerable<GridLocation> GetTiles()
    {
        _visitor.Position = _pointer.GetPosition();
        _visitor.Size = Size;
        return Grid.Accept(_visitor);
    }
}
