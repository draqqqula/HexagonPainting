using HexagonPainting.Core.Grid;
using HexagonPainting.Core.Grid.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Grid.Visitors.Base;

public abstract class PositionVisitorBase<TResult> : IGridVisitor<TResult>
{
    public Vector2 Position { get; set; }
    public abstract TResult VisitFlatTop(HexagonGridFlatTop grid);
    public abstract TResult VisitPointyTop(HexagonGridPointyTop grid);
}
