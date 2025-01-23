using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Models;

namespace HexagonPainting.Core.Map.Interfaces;

public interface IHexagonMap<TValue> : IEnumerable<TileValue<TValue>>
{
    TValue this[GridLocation position] { get; set; }
}
