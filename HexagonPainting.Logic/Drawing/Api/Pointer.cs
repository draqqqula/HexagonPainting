using HexagonPainting.Logic.Drawing.Interfaces;
using System.Numerics;

namespace HexagonPainting.Logic.Drawing.Api;

public class Pointer : IPointer
{
    public Vector2 Position { get; set; }
    public Vector2 GetPosition()
    {
        return Position;
    }
}
