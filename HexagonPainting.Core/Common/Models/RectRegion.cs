using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Core.Common.Models;

public struct RectRegion
{
    public required int MinQ {  get; init; }
    public required int MaxQ { get; init; }
    public required int MinR { get; init; }
    public required int MaxR { get; init; }
}
