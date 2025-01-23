using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Core.Map.Models;
using System.Collections;

namespace HexagonPainting.Logic.Map.Base;

public abstract class HexagonBitMapBase : IHexagonMap<bool?>
{
    private readonly BitArray _data;

    public HexagonBitMapBase(BitArray data)
    {
        _data = data;
    }

    public abstract bool TryGetIndex(int q, int r, out int index);

    public abstract bool TryGetLocation(int index, out GridLocation location);

    public IEnumerator<TileValue<bool?>> GetEnumerator()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            if (TryGetLocation(i, out GridLocation location))
            {
                yield return new TileValue<bool?>()
                {
                    Location = location,
                    Value = _data[i]
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

    public bool? this[GridLocation location]
    {
        get
        {
            return this[location.Q, location.R];
        }
        set
        {
            this[location.Q, location.R] = value;
        }
    }
}

