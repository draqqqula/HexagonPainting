using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Grid.Visitors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Brushes.Base;

public class PointerBrushBase<TColor, TVisitor> : MonoColorBrushBase<TColor> where TVisitor : PositionVisitorBase<IEnumerable<GridLocation>>, new()
{
    public PointerBrushBase(ISelectedValueProvider<TColor> selectedColor, IGrid grid, IPointer pointer) : base(selectedColor, grid)
    {
        Pointer = pointer;
        Visitor = new TVisitor();
    }
    protected IPointer Pointer { get; private set; }
    protected TVisitor Visitor { get; private set; }

    public override IEnumerable<GridLocation> GetTiles()
    {
        Visitor.Position = Pointer.GetPosition();
        return Grid.Accept(Visitor);
    }
}
