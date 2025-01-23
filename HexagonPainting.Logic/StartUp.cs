using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic.Drawing.Colors;
using HexagonPainting.Logic.Map;
using HexagonPainting.Logic.Map.Base;
using Microsoft.Extensions.DependencyInjection;

namespace HexagonPainting.Logic;

public static class StartUp
{
    public static void AddLogic(this IServiceCollection services)
    {
        services.AddTransient<RectangleShapedHexagonBitMap>();
        services.AddTransient<HexagonBitMapBase>(provider => provider.GetRequiredService<RectangleShapedHexagonBitMap>());
        services.AddTransient<IHexagonMap<BitColor>>(provider => provider.GetRequiredService<HexagonBitMapBase>());
    }
}
