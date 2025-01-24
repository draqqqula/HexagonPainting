﻿using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Brushes.Base;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Grid.Visitors;

namespace HexagonPainting.Logic.Drawing.Brushes;

public class CircleBrush<TColor> : MonoColorBrushBase<TColor>
{
    private readonly IPointer _pointer;
    public CircleBrush(ISelectedValueProvider<TColor> selectedColor, IGrid grid, IPointer pointer) : base(selectedColor, grid)
    {
        _pointer = pointer;
    }

    public float Radius { get; set; } = 15f;

    public override IEnumerable<GridLocation> GetTiles()
    {
        return Grid.Accept(new InsideCircleVisitor()
        {
            Position = _pointer.GetPosition(),
            Radius = Radius,
        });
    }
}
