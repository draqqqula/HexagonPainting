using HexagonPainting.Core.Models;

namespace HexagonPainting.Core.Interfaces;

public interface IHexagonMap<TValue>
{
    TValue this[GridLocation position] { get; set; }
}
