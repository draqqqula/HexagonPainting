using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Core.Map.Models;
using HexagonPainting.Logic.Drawing.Colors;
using System.Collections;

namespace HexagonPainting.Logic.Map.Base;

public abstract class HexagonBitMapBase : IHexagonMap<BitColor>
{
    private readonly BitArray _data;

    public HexagonBitMapBase(BitArray data)
    {
        _data = data;
    }

    public abstract bool TryGetIndex(int q, int r, out int index);

    public abstract bool TryGetLocation(int index, out GridLocation location);

    public IEnumerator<TileValue<BitColor>> GetEnumerator()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            if (TryGetLocation(i, out GridLocation location))
            {
                yield return new TileValue<BitColor>()
                {
                    Location = location,
                    Color = BitColorHelper.FromBoolean(_data[i])
                };
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool? this[int q, int r]
    {
        get
        {
            if (TryGetIndex(q, r, out var index))
            {
                return _data.Get(index);
            }
            return default;
        }
        set
        {
            if (value is not null && TryGetIndex(q, r, out var index))
            {
                _data.Set(index, value.Value);
            }
        }
    }

    public BitColor this[GridLocation location]
    {
        get
        {
            return BitColorHelper.FromNullableBoolean(this[location.Q, location.R]);
        }
        set
        {
            this[location.Q, location.R] = value.IsTrue();
        }
    }
}

