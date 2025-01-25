using System.Collections;
using System.Reflection;
using HexagonPainting.Core.Common.Extensions;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Logic.Map.Maps.Base;

namespace HexagonPainting.Logic.Map.Maps;

public class RectangleShapedHexagonBitMap : HexagonBitMapBase
{
    private RectRegion _rect;
    public RectangleShapedHexagonBitMap(RectRegion rect) : base(new BitArray(rect.Area))
    {
        _rect = rect;
    }

    public override void Deserialize(BinaryReader reader)
    {
        _rect = new RectRegion()
        {
            MinQ = reader.ReadInt32(),
            MinR = reader.ReadInt32(),
            MaxQ = reader.ReadInt32(),
            MaxR = reader.ReadInt32(),
        };
        byte b = 0;
        var c = 0;

        _data.Length = _rect.Area;

        for (int i = 0; i < _data.Length; i += 1)
        {

            if (c == 0)
            {
                b = reader.ReadByte();
            }

            var bit = (b & (1 << c)) != 0;
            if (bit)
            {
                _data.Set(i, true);
            }

            c += 1;
            if (c >= 8)
            {
                c = 0;
                b = 0;
            }
        }
    }

    public override void Serialize(BinaryWriter writer)
    {
        writer.Write(_rect.MinQ);
        writer.Write(_rect.MinR);
        writer.Write(_rect.MaxQ);
        writer.Write(_rect.MaxR);
        byte b = 0;
        var c = 0;

        for (int i = 0; i < _data.Length; i += 1)
        {

            if (_data[i])
            {
                b += Convert.ToByte(Math.Pow(2, c));
            }
            c += 1;
            if (c >= 8)
            {
                writer.Write(b);
                c = 0;
                b = 0;
            }
        }
        if (c != 0)
        {
            writer.Write(b);
        }
    }

    public override bool TryGetIndex(int q, int r, out int index)
    {
        return _rect.TryGetIndex(q, r, out index);
    }

    public override bool TryGetLocation(int index, out GridLocation location)
    {
        return _rect.TryGetLocation(index, out location);
    }
}
