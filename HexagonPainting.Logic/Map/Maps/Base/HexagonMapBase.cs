using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Map.Maps.Base;

public abstract class HexagonMapBase
{
    public abstract bool TryGetIndex(int q, int r, out int index);

    public abstract bool TryGetLocation(int index, out GridLocation location);
}
