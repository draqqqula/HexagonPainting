using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting_ViewModels.Services.Interfaces;

public interface IPositionTranslator
{
    public Vector2 Vector { get; init; }
}
