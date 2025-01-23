using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Drawing.Brushes.Base;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Grid.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Brushes;

public class CircleBrush<TColor> : MonoColorBrushBase<TColor, InsideCircleVisitor>
{
    private readonly IPointer _pointer;
    public CircleBrush(ISelectedColor<TColor> selectedColor, IGrid grid, IPointer pointer) : base(selectedColor, grid)
    {
        _pointer = pointer;
    }

    public override InsideCircleVisitor GetVisitor()
    {
        return new InsideCircleVisitor()
        {
            Position = _pointer.GetPosition(),
            Radius = 5f
        };
    }
}
