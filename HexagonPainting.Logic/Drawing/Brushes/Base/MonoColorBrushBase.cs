using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Figure;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Grid.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Brushes.Base;

public abstract class MonoColorBrushBase<TColor, TVisitor> : IBrush<TColor> where TVisitor : IGridVisitor<IEnumerable<GridLocation>>
{
    private readonly ISelectedValueProvider<TColor> _selectedColor;
    private readonly IGrid _grid;

    public MonoColorBrushBase(ISelectedValueProvider<TColor> selectedColor, IGrid grid)
    {
        _selectedColor = selectedColor;
        _grid = grid;
    }

    public abstract TVisitor GetVisitor();

    public IFigure<TColor> Draw()
    {
        var visitor = GetVisitor();
        return new MonoColorFigure<TColor>()
        {
            Color = _selectedColor.Value,
            Region = _grid.Accept(visitor)
        };
    }
}
