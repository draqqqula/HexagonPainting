using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Core.Common.Interfaces;

public interface IBinarySerializer<in T>
{
    public void Serialize(BinaryWriter writer, T value);
}
