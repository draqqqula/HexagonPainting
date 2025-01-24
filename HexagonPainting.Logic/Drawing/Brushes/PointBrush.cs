using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Brushes.Base;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Grid.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Brushes;

public class PointBrush<TColor> : MonoColorBrushBase<TColor>
{
    private readonly GetTileLocationVisitor _visitor;
    private readonly IPointer _pointer;

    public PointBrush(IPointer pointer, ISelectedValueProvider<TColor> selectedColor, IGrid grid) : base(selectedColor, grid)
    {
        _pointer = pointer;
        _visitor = new GetTileLocationVisitor();
    }

    public override IEnumerable<GridLocation> GetTiles()
    {
        _visitor.Position = _pointer.GetPosition();
        yield return Grid.Accept(_visitor);
    }
}
