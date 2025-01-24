using HexagonPainting.Core.Drawing;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Grid.Visitors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Api;

public class Layer<TColor> : IEnumerable<Tile<TColor>>
{
    private readonly IGrid _grid;
    private readonly ISelectedValueProvider<IBrush<TColor>> _brush;
    private readonly TranslatePositionVisitor _visitor;

    public Layer(IHexagonMap<TColor> map, IGrid grid, ISelectedValueProvider<IBrush<TColor>> brush)
    {
        Map = map;
        _grid = grid;
        _brush = brush;
        _visitor = new TranslatePositionVisitor();
    }

    public IHexagonMap<TColor> Map { get; private set; }

    public IEnumerator<Tile<TColor>> GetEnumerator()
    {
        foreach (var tile in Map)
        {
            _visitor.Location = tile.Location;
            var position = _grid.Accept(_visitor);
            yield return new Tile<TColor>()
            {
                Color = tile.Color,
                Position = position,
            };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Draw()
    {
        DrawingHelper.Draw(Map, _brush.Value);
    }
}
