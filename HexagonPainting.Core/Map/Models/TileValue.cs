using HexagonPainting.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Core.Map.Models;

public struct TileValue<T>
{
    public required GridLocation Location { get; init; }
    public required T Value { get; init; }
}
