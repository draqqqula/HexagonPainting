using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace HexagonPainting_ViewModels;

public class Hex
{
    public Point Coordinates { get; set; }
    public double Scale { get; set; }
    public Color Color { get; set; }
}
