using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Grid;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Core.Map.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Grid.Visitors;

public class TranslatePositionVisitor : IGridVisitor<Vector2>
{
    public GridLocation Location { get; set; }
    public Vector2 VisitFlatTop(HexagonGridFlatTop grid)
    {
        throw new NotImplementedException();
    }

    public Vector2 VisitPointyTop(HexagonGridPointyTop grid)
    {
        return new Vector2(Location.Q * grid.HalfWidth + Location.R * grid.HalfWidth * 2, Location.Q * grid.CellSide * 1.5f);
    }
}
