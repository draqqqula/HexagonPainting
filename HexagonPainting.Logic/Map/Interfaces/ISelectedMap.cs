using HexagonPainting.Core.Map.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Map.Interfaces;

public interface ISelectedMap<TColor>
{
    public IHexagonMap<TColor> Value { get; }
}
