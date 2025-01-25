using FakeItEasy;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Logic;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Api;
using HexagonPainting.Logic.Drawing.Brushes;
using HexagonPainting.Logic.Drawing.Colors;
using HexagonPainting.Logic.Drawing.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Numerics;
using Avalonia;
using Avalonia.Media;
using HexagonPainting.Core.Common.Interfaces;
using HexagonPainting.Logic.Map.Maps;
using HexagonPainting.Core.Map.Interfaces;

namespace HexagonPainting.Tests.Logic;

[TestFixture]
public class AvaloniaColorDrawingTests
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

    private IServiceProvider _provider;
    private IPointer _pointer;
    private Selected<Color> _selectedColor;
    private Selected<IBrush<Color>> _selectedBrush;
    private Layer<Color> _mainLayer;


    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        var factory = new DefaultServiceProviderFactory();

        _pointer = A.Fake<IPointer>();

        services.AddSingleton(_pointer);

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
            }, Colors.SkyBlue));
        services.AddServicesFor(Colors.White);

        _provider = factory.CreateServiceProvider(services);

        _selectedColor = _provider.GetRequiredService<Selected<Color>>();
        _selectedBrush = _provider.GetRequiredService<Selected<IBrush<Color>>>();
        _mainLayer = _provider.GetRequiredKeyedService<Layer<Color>>("main");
    }

    [TestCase(0, 0, 0f, 0f, true)]
    [TestCase(1, 1, 0f, 0f, true)]
    [TestCase(1, 1, 10f, 10f, true)]
    [TestCase(0, 0, 10f, 10f, false)]
    public void CircleBrushTest(int q, int r, float x, float y, bool shouldBeTrue)
    {
        var brush = _provider.GetRequiredService<CircleBrush<Color>>();
        _selectedBrush.Value = brush;
        _selectedColor.Value = Colors.Red;

        A.CallTo(() => _pointer.GetPosition()).Returns(new Vector2(x, y));
        _mainLayer.Draw();
        var location = new GridLocation()
        {
            Q = q,
            R = r
        };
        if (shouldBeTrue)
        {
            Assert.That(_mainLayer.Map[location] == Colors.Red);
        }
        else
        {
            Assert.That(_mainLayer.Map[location] != Colors.Red);
        }
    }
}