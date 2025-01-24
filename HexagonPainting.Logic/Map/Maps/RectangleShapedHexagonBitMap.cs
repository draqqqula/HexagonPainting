using System.Collections;
using System.Reflection;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Logic.Map.Maps.Base;

namespace HexagonPainting.Logic.Map.Maps;

public class RectangleShapedHexagonBitMap : HexagonBitMapBase
{
    private int _minQ;
    private int _maxQ;
    private int _minR;
    private int _maxR;
    public RectangleShapedHexagonBitMap(BitArray data, int minQ, int minR, int maxQ, int maxR) : base(data)
    {
        _minQ = minQ;
        _maxQ = maxQ;
        _minR = minR;
        _maxR = maxR;
    }

    public override void Deserialize(BinaryReader reader)
    {
        _minQ = reader.ReadInt32();
        _minR = reader.ReadInt32();
        _maxQ = reader.ReadInt32();
        _maxR = reader.ReadInt32();
        byte b = 0;
        var c = 0;

        _data.Length = (_maxQ - _minQ) * (_maxR - _minR);

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
        writer.Write(_minQ);
        writer.Write(_minR);
        writer.Write(_maxQ);
        writer.Write(_maxR);
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
        var half = Convert.ToInt32(MathF.Floor((float)q / 2));
        var r0 = r + half;
        if (q < _minQ || q >= _maxQ || r0 < _minR || r0 >= _maxR)
        {
            index = 0;
            return false;
        }
        index = (q - _minQ) * (_maxR - _minR) + r0 - _minR;
        return true;
    }

    public override bool TryGetLocation(int index, out GridLocation location)
    {
        if (index < 0 || index > (_maxQ - _minQ) * (_maxR - _minR))
        {
            location = new GridLocation()
            {
                Q = 0,
                R = 0
            };
            return false;
        }
        var lineLength = _maxR - _minR;
        var line = Convert.ToInt32(MathF.Floor((float)index / lineLength)); ;
        var q = _minQ + line;
        var half = Convert.ToInt32(MathF.Floor((float)q / 2));
        var r = -half + index & line;
        location = new GridLocation()
        {
            Q = q,
            R = r
        };
        return true;
    }
}
