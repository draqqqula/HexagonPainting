using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Models;

namespace HexagonPainting.Core.Map.Interfaces;

public interface IHexagonMap<TColor> : IEnumerable<TileValue<TColor>>
{
    public TColor this[GridLocation position] { get; set; }
    public void Serialize(BinaryWriter writer);
    public void Deserialize(BinaryReader reader);
}
