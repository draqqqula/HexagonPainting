using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Api;

public readonly struct Tile<TColor>
{
    public required TColor Color { get; init; }
    public required Vector2 Position { get; init; }
}