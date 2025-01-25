using Avalonia;
using System;
using System.IO;
using Avalonia.Media;
using CommunityToolkit.Mvvm.DependencyInjection;
using HexagonPainting.Core.Common.Interfaces;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic;
using HexagonPainting.Logic.Drawing.Interfaces;
using HexagonPainting.Logic.Map.Maps;
using HexagonPainting.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HexagonPainting
{
    internal sealed class Program
    {
        class ColorSerializer : IBinarySerializer<Color>
        {
            public void Serialize(BinaryWriter writer, Color value)
            {
                writer.Write(value.ToUInt32());
            }

            public void Serialize(BinaryWriter writer, Color value)
            {
                throw new NotImplementedException();
            }
        }

        class ColorDeserializer : IBinaryDeserializer<Color>
        {
            public Color Deserialize(BinaryReader reader)
            {
                return Color.FromUInt32(reader.ReadUInt32());
            }
        }
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var colorSerializer = new ColorSerializer();
            
            
            var services = new ServiceCollection();
            services.AddSingleton<IPointer, >();
            services.AddSingleton()

            services.AddSingleton<IBinarySerializer<Color>, ColorSerializer>();
            services.AddSingleton<IBinaryDeserializer<Color>, ColorDeserializer>();
            services.AddRectangleMapFactory<Color>();

            services.AddLogic();
            services.AddBrushesFor<Color>();
            services.AddSingleton<IHexagonMap<Color>>(
                provider => provider.GetRequiredService<RectangleShapedHexagonMapFactory<Color>>().FromRect(new RectRegion()
                {
                    MinQ = -16,
                    MinR = -16,
                    MaxQ = 16,
                    MaxR = 16
                }));
            services.AddServicesFor(Colors.White);
            var provider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(provider);

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();
    }
}