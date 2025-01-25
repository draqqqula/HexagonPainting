using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HexagonPainting.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using HexagonPainting.Logic.Drawing.Api;
using Pointer = HexagonPainting.Logic.Drawing.Api.Pointer;
using Microsoft.Extensions.DependencyInjection;
using HexagonPainting_ViewModels;
using HexagonPainting_ViewModels.Services;
using HexagonPainting.Logic.Common;
using System.Numerics;

namespace HexagonPainting.Controls
{
    public class PaintControl : Control
    {
        private Pointer _pointer;
        private Layer<Color> _mainLayer;
        private Selected<Color> _color;
        private Point _offset = new Point(0, 0);
        private float _scale = 10f;

        public PaintControlViewModel? Vm => DataContext as PaintControlViewModel;

        public PaintControl()
        {
            _pointer = Services.Default.GetRequiredService<Pointer>();
            _mainLayer = Services.Default.GetRequiredKeyedService<Layer<Color>>("main");
            _color = Services.Default.GetRequiredService<Selected<Color>>();
            // Setup event handlers
            PointerMoved += PaintControl_PointerMoved;
            PointerPressed += PaintControl_PointerPressed;
            PointerReleased += PaintControl_PointerReleased;
            PointerCaptureLost += PaintControl_PointerCaptureLost;
            SizeChanged += PaintControl_SizeChanged;

            KeyDownEvent.AddClassHandler<TopLevel>(PaintControl_KeyDown, handledEventsToo: true);
            KeyUpEvent.AddClassHandler<TopLevel>(PaintControl_KeyUp, handledEventsToo: true);

            _mainLayer.OnMapStateChanged += InvalidateVisual;
            _color.Value = Colors.Black;
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == DataContextProperty && Bounds.Size != default)
            {
                // let the view model know the PaintControl's size, so the image can be the correct size.
                Vm?.SetImageSize(new PixelSize((int)Bounds.Width, (int)Bounds.Height));

                // Request the image be rendered
                InvalidateVisual();
            }
        }

        private void PaintControl_PointerMoved(object? sender, PointerEventArgs e)
        {
            var pos = e.GetPosition(this);

            _pointer.Position = new System.Numerics.Vector2((float)pos.X, (float)pos.Y) / _scale;

            if (Vm != null)
            {
                Vm.Pos = pos;
                if (Vm.Dragging)
                {
                    _mainLayer.Draw();
                    InvalidateVisual();
                }

                e.Handled = true;
            }
        }

        private void PaintControl_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            _mainLayer.Draw();

            if (Vm != null)
            {
                // Start the drag
                Vm.Dragging = true;
            }
        }

        private void PaintControl_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (Vm != null)
            {
                if (Vm.Dragging == true)
                {
                    // Finish dragging
                    Vm.Dragging = false;

                    // Paint a new rectangle
                    Vm.AddRectangle();

                    // Request the updated image be rendered
                    InvalidateVisual();
                }
            }
        }

        public void DrawHexes(IEnumerable<Hex> hexes)
        {
            if (Vm == null)
                return;
            Vm.AddHex(hexes);

            // Request the updated image be rendered
            InvalidateVisual();
        }


        private void PaintControl_PointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
        {
            if (Vm != null)
            {
                // finish Dragging
                Vm.Dragging = false;

                // Request the image be rendered (to clear any marquee)
                InvalidateVisual();
            }
        }

        private void PaintControl_SizeChanged(object? sender, SizeChangedEventArgs e)
        {
            if (Vm != null)
            {
                // Make sure the image matches the size of the control
                Vm.SetImageSize(new PixelSize((int)e.NewSize.Width, (int)e.NewSize.Height));

                // Request the updated image be rendered
                InvalidateVisual();
            }
        }
        public void SaveMapToFile()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "map.bin");
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fileStream))
            {
                _mainLayer.Map.Serialize(writer);
            }
        }
        public void LoadMapFromFile()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "map.bin");
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fileStream))
            {
                _mainLayer.Map.Deserialize(reader);
            }
            InvalidateVisual();
        }

        private void PaintControl_KeyDown(object? sender, KeyEventArgs e)
        {
            if (Vm != null)
            {
                // Change rectangle color or cancel dragging
                // Request the updated image be rendered, in case there is a marquee
                switch (e.Key)
                {
                    case Key.LeftShift:
                        Vm.Red = 255;
                        InvalidateVisual();
                        break;

                    case Key.LeftCtrl:
                        Vm.Green = 255;
                        InvalidateVisual();
                        break;
                    case Key.O:
                        SaveMapToFile();
                        break;
                    case Key.I:
                        LoadMapFromFile();
                        break;
                    case Key.LeftAlt:
                        Vm.Blue = 255;
                        InvalidateVisual();
                        break;

                    case Key.Escape:
                        Vm.Dragging = false;
                        InvalidateVisual();
                        break;
                }
            }

            _color.Value = Color.FromRgb(Vm.Red, Vm.Green, Vm.Blue);
        }

        private void PaintControl_KeyUp(object? sender, KeyEventArgs e)
        {
            if (Vm != null)
            {
                // Change rectangle color
                // Request the updated image be rendered, in case there is a marquee
                switch (e.Key)
                {
                    case Key.LeftShift:
                        Vm.Red = 0;
                        InvalidateVisual();
                        break;

                    case Key.LeftCtrl:
                        Vm.Green = 0;
                        InvalidateVisual();
                        break;

                    case Key.LeftAlt:
                        Vm.Blue = 0;
                        InvalidateVisual();
                        break;
                }
            }
            _color.Value = Color.FromRgb(Vm.Red, Vm.Green, Vm.Blue);
        }


        /// <summary>
        /// Render the saved graphic, and marquee when needed
        /// </summary>
        /// <param name="context"></param>
        public override void Render(DrawingContext context)
        {
            DrawMainLayer();
            // If there is an image in the view model, copy it to the PaintControl's drawing surface
            if (Vm?.Image != null)
            {
                context.DrawImage(Vm.Image, Bounds);
            }

            // If we are in a dragging operation, draw a dashed rectangle
            // The base color for the rectangle is the color for rectangle that the drag operation will draw
            // The alternative color is either black or white to contrast with the base color
            if (Vm?.Dragging == true)
            {
                var pen = new Pen(new SolidColorBrush(Color.FromRgb(Vm.Red, Vm.Green, Vm.Blue)));
            }
        }

        public void DrawMainLayer()
        {
            var hexes = _mainLayer.Select(it => new Hex()
            {
                Coordinates = _offset + new Point(it.Position.X, it.Position.Y) * (_scale * (Math.Sqrt(3) * 0.5f)),
                Scale = _scale,
                Color = it.Color
            });
            Vm.AddHex(hexes);
        }
    }
}