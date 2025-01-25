using Avalonia.Media.Imaging;
using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Immutable;
using HexagonPainting_ViewModels;
using HexagonPainting_ViewModels.Services;
using Microsoft.Extensions.DependencyInjection;
using HexagonPainting.Logic.Drawing.Api;

namespace HexagonPainting.ViewModels
{
    public class PaintControlViewModel : INotifyPropertyChanged
    {
        private bool _dragging = false;
        private Point _origin = new Point(0, 0);
        private Point _pos = new Point(0, 0);
        private Rect _marquee = new Rect();
        private RenderTargetBitmap? _image = null;

        // Are we in a drag operation?
        public bool Dragging
        {
            get => _dragging;
            set
            {
                if (_dragging != value)
                {
                    _dragging = value;
                    OnPropertyChanged(nameof(Dragging));
                }
            }
        }

        // The current mouse position
        public Point Pos
        {
            get => _pos;
            set
            {
                if (_pos != value)
                {
                    _pos = value;
                    OnPropertyChanged(nameof(Pos));
                }
            }
        }

        // The color for the rectangle to be drawn
        // This can be controlled by pressing Shift, Ctrl and Alt keys
        public byte Red { get; set; } = 0;
        public byte Green { get; set; } = 0;
        public byte Blue { get; set; } = 0;

        // The bitmap full of rectangles
        public IImage? Image
        {
            get => _image;
        }

        // When the control changes, we need to change the bitmap to match
        public void SetImageSize(PixelSize size)
        {
            RenderTargetBitmap newImage = new RenderTargetBitmap(new PixelSize(size.Width, size.Height), new Vector(96, 96));
            if (_image != null)
            {
                // If there was already a bitmap, copy it into the new one.
                using (var context = newImage.CreateDrawingContext())
                {
                    context.DrawImage(_image, new Rect(0, 0, size.Width, size.Height), new Rect(0, 0, size.Width, size.Height));
                }
            }
            _image = newImage;
            OnPropertyChanged(nameof(Image));
        }
        public void AddHex(IEnumerable<Hex> hexes)
        {
            if (_image != null)
            {
                // Создаем новое изображение, копируем старое и добавляем шестиугольники
                RenderTargetBitmap newImage = new RenderTargetBitmap(_image.PixelSize, _image.Dpi);
                using (var context = newImage.CreateDrawingContext())
                {
                    //context.DrawImage(_image, new Rect(0, 0, _image.PixelSize.Width, _image.PixelSize.Height));

                    foreach (var hex in hexes)
                    {
                        DrawHexagon(context, hex);
                    }
                }

                _image.Dispose();
                _image = newImage;
                OnPropertyChanged(nameof(Image));
            }
        }
        private void DrawHexagon(DrawingContext context, Hex hex)
        {
            var hexPath = new PathGeometry();
            var hexFigure = new PathFigure { StartPoint = GetHexCorner(hex.Coordinates, hex.Scale, 0) };

            for (int i = 1; i < 6; i++)
            {
                hexFigure.Segments.Add(new LineSegment { Point = GetHexCorner(hex.Coordinates, hex.Scale, i) });
            }
            hexFigure.IsClosed = true;
            hexPath.Figures.Add(hexFigure);

            var pen = new Pen(new SolidColorBrush(Color.FromRgb(Red, Green, Blue)));
            var brush = new ImmutableSolidColorBrush(new SolidColorBrush(Color.FromRgb(Red, Green, Blue)));
            context.DrawGeometry(brush, pen, hexPath);
        }

        private Point GetHexCorner(Point center, double scale, int corner)
        {
            double angleDeg = 60 * corner + 30;
            double angleRad = Math.PI / 180 * angleDeg;
            return new Point(center.X + scale * Math.Cos(angleRad), center.Y + scale * Math.Sin(angleRad));
        }

        public void AddRectangle()
        {
            if (_image != null)
            {
                // Create a new image, copy the old one, and then add a new rectangle to it
                // using the current marquee and color
                RenderTargetBitmap newImage = new RenderTargetBitmap(_image.PixelSize, _image.Dpi);
                using (var context = newImage.CreateDrawingContext())
                {
                    context.DrawImage(_image, new Rect(0, 0, _image.PixelSize.Width, _image.PixelSize.Height));
                    var brush = new SolidColorBrush(Color.FromRgb(Red, Green, Blue));
                    context.FillRectangle(brush, _marquee);
                }
                _image.Dispose();
                _image = newImage;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

