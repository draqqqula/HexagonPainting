namespace HexagonPainting.Core.Common.Models;

public readonly struct GridLocation
{
    public GridLocation(int q, int r)
    {
        Q = q;
        R = r;
    }

    public required int Q { get; init; }
    public required int R { get; init; }
    public int S => -Q - R;
}