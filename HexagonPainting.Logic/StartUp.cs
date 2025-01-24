using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Core.Grid;
using HexagonPainting.Core.Grid.Interfaces;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic.Drawing.Brushes;
using HexagonPainting.Logic.Drawing.Colors;
using HexagonPainting.Logic.Map;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Api;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Logic.Map.Maps;
using System.Collections;

namespace HexagonPainting.Logic;

public static class StartUp
{
    public static void AddLogic(this IServiceCollection services)
    {
        services.AddCommonDefaults();

        services.AddBrushesFor<BitColor>();
        services.AddSelectables<BitColor>(
            BitColor.True, 
            new RectangleShapedHexagonBitMap(new BitArray(1024), -16, -16, 16, 16));
    }

    public static void AddCommonDefaults(this IServiceCollection services)
    {
        services.AddSingleton<IGrid>(provider => new HexagonGridPointyTop(1f));
    }

    public static void AddBrushesFor<TColor>(this IServiceCollection services)
    {
        services.AddTransient<IBrush<TColor>, CircleBrush<TColor>>();
    }

    public static void AddSelectables<TColor>(this IServiceCollection services, 
        TColor defaultColor, IHexagonMap<TColor> defaultMap)
    {
        services.AddSingleton<IHexagonMap<TColor>>(defaultMap);
        services.AddSingleton<Selected<TColor>>(new Selected<TColor>(defaultColor));
        services.AddSingleton<Selected<IBrush<TColor>>>();
        services.AddSingleton<Selected<IHexagonMap<TColor>>>();
        services.AddSingleton<ISelectedValueProvider<TColor>>(it => it.GetRequiredService<Selected<TColor>>());
        services.AddSingleton<ISelectedValueProvider<IBrush<TColor>>>(it => it.GetRequiredService<Selected<IBrush<TColor>>>());
        services.AddSingleton<ISelectedValueProvider<IHexagonMap<TColor>>>(it => it.GetRequiredService<Selected<IHexagonMap<TColor>>>());
        services.AddSingleton<DrawingController<TColor>>();
    }

    public static void AddDefaultRectangleMapFor<TColor>(this IServiceCollection services, RectRegion region)
    {
        services.AddTransient<IHexagonMap<TColor>>();
    }
}
