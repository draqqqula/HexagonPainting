using Avalonia.Media;
using HexagonPainting.Core.Common.Interfaces;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic;
using HexagonPainting.Logic.Map.Maps;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonPainting_ViewModels;

public static class StartUp
{
    class ColorSerializer : IBinarySerializer<Color>
    {
        public void Serialize(BinaryWriter writer, Color value)
        {
            writer.Write(value.ToUInt32());
        }
    }

    class ColorDeserializer : IBinaryDeserializer<Color>
    {
        public Color Deserialize(BinaryReader reader)
        {
            return Color.FromUInt32(reader.ReadUInt32());
        }
    }
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<IBinarySerializer<Color>, ColorSerializer>();
        services.AddSingleton<IBinaryDeserializer<Color>, ColorDeserializer>();
        services.AddRectangleMapFactory<Color>();

        services.AddLogic();
        services.AddBrushesFor<Color>();
        services.AddTransient<IHexagonMap<Color>>(
            provider => provider.GetRequiredService<RectangleShapedHexagonMapFactory<Color>>().FromRect(new RectRegion()
            {
                MinQ = -32,
                MinR = -32,
                MaxQ = 32,
                MaxR = 32
            }, Colors.SkyBlue));
        services.AddServicesFor(Colors.White);
    }
}
