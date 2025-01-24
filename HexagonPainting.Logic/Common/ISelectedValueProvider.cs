using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Common;

public interface ISelectedValueProvider<T>
{
    public T Value { get; }
}
