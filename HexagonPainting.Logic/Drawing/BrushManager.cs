using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Logic.Drawing.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing;

public class BrushManager<T> : IBrushManager
{
    private readonly Dictionary<Type, IBrush<T>> BrushByType;
    public BrushManager(IEnumerable<IBrush<T>> brushes)
    {
        BrushByType = brushes.ToDictionary(it => it.GetType());
    }
}
