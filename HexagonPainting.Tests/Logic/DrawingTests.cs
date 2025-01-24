using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using HexagonPainting.Logic;
using FakeItEasy;
using HexagonPainting.Logic.Drawing.Interfaces;
using System.Numerics;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic.Drawing.Colors;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Drawing.Interfaces;
using HexagonPainting.Logic.Drawing.Brushes;
using HexagonPainting.Core.Drawing;
using HexagonPainting.Logic.Map.Maps;
using System.Collections;
using HexagonPainting.Logic.Common;
using HexagonPainting.Logic.Drawing.Api;

namespace HexagonPainting.Tests.Logic;

[TestFixture]
public class DrawingTests
{
    private IServiceProvider _provider;
    private IPointer _pointer;
    private Selected<BitColor> _selectedColor;
    private Selected<IBrush<BitColor>> _selectedBrush;
    private Layer<BitColor> _mainLayer;


    [SetUp]
    public void SetUp()
    {
        var services = new ServiceCollection();
        var factory = new DefaultServiceProviderFactory();

        _pointer = A.Fake<IPointer>();

        services.AddSingleton(_pointer);
        services.AddLogic();

        _provider = factory.CreateServiceProvider(services);

        _selectedColor = _provider.GetRequiredService<Selected<BitColor>>();
        _selectedBrush = _provider.GetRequiredService<Selected<IBrush<BitColor>>>();
        _mainLayer = _provider.GetRequiredKeyedService<Layer<BitColor>>("main");
    }

    [TestCase(0, 0, 0f, 0f, true)]
    [TestCase(1, 1, 0f, 0f, true)]
    [TestCase(1, 1, 10f, 10f, true)]
    [TestCase(0, 0, 10f, 10f, false)]
    public void CircleBrushTest(int q, int r, float x, float y, bool shouldBeTrue)
    {
        var brush = _provider.GetRequiredService<CircleBrush<BitColor>>();
        _selectedBrush.Value = brush;

        A.CallTo(() => _pointer.GetPosition()).Returns(new Vector2(x, y));
        _mainLayer.Draw();
        var location = new GridLocation()
        {
            Q = q,
            R = r
        };
        if (shouldBeTrue)
        {
            Assert.That(_mainLayer.Map[location] == BitColor.True);
        }
        else
        {
            Assert.That(_mainLayer.Map[location] == BitColor.False);
        }
    }
}