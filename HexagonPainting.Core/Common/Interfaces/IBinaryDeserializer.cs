using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Core.Common.Interfaces;

public interface IBinaryDeserializer<out T>
{
    public T Deserialize(BinaryReader reader);
}
