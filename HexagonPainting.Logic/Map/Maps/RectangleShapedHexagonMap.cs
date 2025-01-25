using HexagonPainting.Core.Common.Extensions;
using HexagonPainting.Core.Common.Interfaces;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Core.Map.Models;
using HexagonPainting.Logic.Drawing.Colors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Map.Maps;

public class RectangleShapedHexagonMap<TColor> : IHexagonMap<TColor>
{
    private TColor[] _data;
    private RectRegion _rect;
    private IBinarySerializer<TColor> _serializer;
    private IBinaryDeserializer<TColor> _deserializer;

    public RectangleShapedHexagonMap(RectRegion rect, 
        IBinarySerializer<TColor> serializer, 
        IBinaryDeserializer<TColor> deserializer)
    {
        _serializer = serializer;
        _deserializer = deserializer;
        _rect = rect;
        _data = new TColor[rect.Area];
    }

    public TColor this[GridLocation position]
    {
        get
        {
            if (_rect.TryGetIndex(position.Q, position.R, out var index))
            {
                return _data[index];
            }
            return default;
        }
        set
        {
            if (value is not null && _rect. TryGetIndex(position.Q, position.R, out var index))
            {
                _data[index] = value;
            }
        }
    }

    public IEnumerator<TileValue<TColor>> GetEnumerator()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            if (_rect.TryGetLocation(i, out GridLocation location))
            {
                yield return new TileValue<TColor>()
                {
                    Location = location,
                    Color = _data[i]
                };
            }
        }
    }

    public void Deserialize(BinaryReader reader)
    {
        _rect = new RectRegion()
        {
            MinQ = reader.ReadInt32(),
            MinR = reader.ReadInt32(),
            MaxQ = reader.ReadInt32(),
            MaxR = reader.ReadInt32(),
        };
        _data = new TColor[_rect.Area];

        for (int i = 0; i < _data.Length; i += 1)
        {
            _data[i] = _deserializer.Deserialize(reader);
        }
    }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(_rect.MinQ);
        writer.Write(_rect.MinR);
        writer.Write(_rect.MaxQ);
        writer.Write(_rect.MaxR);

        for (int i = 0; i < _data.Length; i += 1)
        {
            _serializer.Serialize(writer, _data[i]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class RectangleShapedHexagonMapFactory<TColor>
{
    private readonly IBinarySerializer<TColor> _serializer;
    private readonly IBinaryDeserializer<TColor> _deserializer;
    public RectangleShapedHexagonMapFactory(IBinarySerializer<TColor> serializer, IBinaryDeserializer<TColor> deserializer)
    {
        _serializer = serializer;
        _deserializer = deserializer;
    }

    public RectangleShapedHexagonMap<TColor> FromRect(RectRegion rect)
    {
        return new RectangleShapedHexagonMap<TColor>(rect, _serializer, _deserializer);
    }
}