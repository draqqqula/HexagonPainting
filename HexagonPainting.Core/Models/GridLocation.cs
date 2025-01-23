namespace HexagonPainting.Core.Models;

public readonly struct GridLocation
{
    public required int Q { get; init; }
    public required int R { get; init; }
    public int S => -Q - R;
}