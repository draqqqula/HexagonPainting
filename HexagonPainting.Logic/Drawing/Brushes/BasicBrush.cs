using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Drawing.Figure;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Grid.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Brushes;

public class BasicBrush<T, TVisitor> : IBrush<T> where TVisitor : IGridVisitor<IEnumerable<GridLocation>>
{
    private readonly ISelectedColor<T> _selectedColor;
    private readonly IGrid _grid;
    private readonly TVisitor _visitor;

    public BasicBrush(ISelectedColor<T> selectedColor, IGrid grid, TVisitor visitor)
    {
        _selectedColor = selectedColor;
        _grid = grid;
        _visitor = visitor;
    }

    public IFigure<T> Draw()
    {
        return new MonoColorFigure<T>()
        {
            Color = _selectedColor.Value,
            Region = _grid.Accept(_visitor)
        };
    }
}
