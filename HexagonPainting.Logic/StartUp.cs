using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic.Map;
using HexagonPainting.Logic.Map.Base;
using Microsoft.Extensions.DependencyInjection;

namespace HexagonPainting.Logic;

public static class StartUp
{
    public static void AddLogic(this IServiceCollection services)
    {
        services.AddTransient<QRCoordinateBasedHexagonMap>();
        services.AddTransient<HexagonBitMapBase>(provider => provider.GetRequiredService<QRCoordinateBasedHexagonMap>());
        services.AddTransient<IHexagonMap<bool?>>(provider => provider.GetRequiredService<HexagonBitMapBase>());
    }
}
