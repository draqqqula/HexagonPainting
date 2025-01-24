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

public abstract class MonoColorBrushBase<TColor> : IBrush<TColor>
{
    private readonly ISelectedValueProvider<TColor> _selectedColor;

    public MonoColorBrushBase(ISelectedValueProvider<TColor> selectedColor, IGrid grid)
    {
        _selectedColor = selectedColor;
        Grid = grid;
    }
    public TColor Color => _selectedColor.Value;
    public IGrid Grid { get; protected init; }
    public abstract IEnumerable<GridLocation> GetTiles();

    public IFigure<TColor> Draw()
    {
        return new MonoColorFigure<TColor>()
        {
            Color = Color,
            Region = GetTiles()
        };
    }
}
