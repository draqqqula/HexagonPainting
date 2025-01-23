using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing.Interfaces;

public interface ISelectedColor<T>
{
    public T Value { get; }
}
