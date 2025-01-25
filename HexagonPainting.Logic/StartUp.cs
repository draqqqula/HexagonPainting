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
        services.AddTransient<IHexagonMap<BitColor>>(provider => new RectangleShapedHexagonBitMap(new RectRegion()
        {
            MinQ = -16,
            MinR = -16,
            MaxQ = 16,
            MaxR = 16
        }));
        services.AddServicesFor(BitColor.True);
    }

    public static void AddCommonDefaults(this IServiceCollection services)
    {
        services.AddSingleton<IGrid>(provider => new HexagonGridPointyTop(1f));
    }

    public static void AddBrushesFor<TColor>(this IServiceCollection services)
    {
        services.AddTransient<CircleBrush<TColor>>();
        services.AddTransient<RectangleBrush<TColor>>();
        services.AddTransient<PointBrush<TColor>>();
        services.AddTransient<IBrush<TColor>>(provider => provider.GetRequiredService<CircleBrush<TColor>>());
        services.AddTransient<IBrush<TColor>>(provider => provider.GetRequiredService<RectangleBrush<TColor>>());
        services.AddTransient<IBrush<TColor>>(provider => provider.GetRequiredService<PointBrush<TColor>>());
    }

    public static void AddServicesFor<TColor>(this IServiceCollection services, 
        TColor defaultColor)
    {
        services.AddSingleton<Selected<TColor>>(new Selected<TColor>(defaultColor));
        services.AddSingleton<Selected<IBrush<TColor>>>();
        services.AddSingleton<ISelectedValueProvider<TColor>>(it => it.GetRequiredService<Selected<TColor>>());
        services.AddSingleton<ISelectedValueProvider<IBrush<TColor>>>(it => it.GetRequiredService<Selected<IBrush<TColor>>>());
        services.AddTransient<Layer<TColor>>();
        services.AddKeyedSingleton<Layer<TColor>>("main");
        services.AddKeyedSingleton<Layer<TColor>>("preview");
    }

    public static void AddRectangleMapFactory<TColor>(this IServiceCollection services)
    {
        services.AddSingleton<RectangleShapedHexagonMapFactory<TColor>>();
    }
}
