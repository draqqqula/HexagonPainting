using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting.Logic.Drawing;

public interface IDrawingController
{
    public void SetColor<TColor>();
    public void Draw();
}
