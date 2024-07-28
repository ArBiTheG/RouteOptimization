using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using DynamicData;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace RouteOptimization.Controls
{
    public partial class MapBuilderControl : Control
    {
        //Цвета
        private IBrush _brushBackground;
        private IBrush _brushVertex;
        private IBrush _brushVertexSelected;

        private IPen _penEdge;

        // Управление сценой
        private bool _pointerHold = false;
        private bool _oneVertexSelected = false;
        private Point _lastPositionPointerPressed;

        // Управление сценой
        double _zoom = 1;
        ScenePoint _scenePosition;
        ScenePoint _sceneOffsetPosition;

        public ObservableCollection<Vertex> Vertices { get; set; }

        public MapBuilderControl()
        {
            _brushBackground = new SolidColorBrush(Color.FromArgb(127, 25, 25, 50));
            _brushVertex = new SolidColorBrush(Color.FromArgb(255,100,100,200));
            _brushVertexSelected = new SolidColorBrush(Color.FromArgb(255, 200, 200, 255));

            _penEdge = new Pen(new SolidColorBrush(Color.FromArgb(255, 50,0,150)));

            Vertices =
            [
                new Vertex(50,50),
                new Vertex(170,170),
                new Vertex(200,500),
            ];

            Vertices[0].Edges.Add(new Edge(Vertices[1]));
            Vertices[1].Edges.Add(new Edge(Vertices[2]));
            Vertices[0].Edges.Add(new Edge(Vertices[2]));

            InitializeComponent();

        }
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            Point pointerPosition = e.GetPosition(this);

            _oneVertexSelected = false;
            _lastPositionPointerPressed = pointerPosition;
            _pointerHold = true;

            foreach (Vertex vertex in Vertices.Reverse())
            {
                PressVertex(vertex, pointerPosition.X, pointerPosition.Y, e.KeyModifiers);
            }

            InvalidateVisual();
        }
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            _pointerHold = false;
            foreach (Vertex v in Vertices)
            {
                v.OldX = v.X;
                v.OldY = v.Y;
            }
            _scenePosition.X += _sceneOffsetPosition.X;
            _scenePosition.Y += _sceneOffsetPosition.Y;
            _sceneOffsetPosition.X = 0;
            _sceneOffsetPosition.Y = 0;
        }
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            Point pointerPosition = e.GetPosition(this);
            foreach (Vertex vertex in Vertices)
            {
                MoveSelectedVertex(vertex, pointerPosition.X, pointerPosition.Y, e.KeyModifiers);
            }
            MoveScene(pointerPosition.X, pointerPosition.Y, e.KeyModifiers);

            InvalidateVisual();
        }

        protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
        {
            if (e.Delta.Y > 0)
            {
                if (_zoom <= 100)
                    _zoom *= 1.10;
            }
            else
            {
                if (_zoom >= 0.05)
                    _zoom /= 1.10;
            }
            InvalidateVisual();
        }

        public override void Render(DrawingContext context)
        {
            if (_brushBackground != null)
            {
                var renderSize = Bounds.Size;
                context.FillRectangle(_brushBackground, new Rect(renderSize));
            };
            var ft = new FormattedText(
                    "Координаты X=" + Math.Round(_scenePosition.X,3) + 
                    " / Y=" + Math.Round(_scenePosition.Y, 3) + 
                    "Масштаб: " + Math.Round(_zoom, 2) * 100 + "%",
                    new CultureInfo(1),
                    FlowDirection.LeftToRight,
                    Typeface.Default,
                    14,
                    Brushes.White);

            context.DrawText(ft, new Point(0, 0));
            foreach (Vertex v in Vertices)
            {
                foreach (Edge e in v.Edges)
                {
                    context.DrawLine(_penEdge, GetSceneDrawPosition(v.X, v.Y), GetSceneDrawPosition(e.Vertex.X, e.Vertex.Y));
                }
                if (v.Selected)
                {
                    context.DrawEllipse(_brushVertexSelected, null, GetSceneDrawPosition(v.X, v.Y), v.Size * _zoom, v.Size * _zoom);
                }
                else
                {
                    context.DrawEllipse(_brushVertex, null, GetSceneDrawPosition(v.X, v.Y), v.Size * _zoom, v.Size * _zoom);
                }
            }
        }


        private void PressVertex(Vertex vertex, double x, double y, KeyModifiers keyModifiers)
        {
            if (PointerInsideVertex(vertex, x, y) &&
                    _oneVertexSelected == false)
            {
                _oneVertexSelected = true;
                vertex.Selected = true;
            }
            else
            {
                if (keyModifiers != KeyModifiers.Shift &&
                    keyModifiers != KeyModifiers.Control)
                    vertex.Selected = false;
            }
        }
        private void MoveSelectedVertex(Vertex vertex, double x, double y, KeyModifiers keyModifiers)
        {
            if (vertex.Selected && keyModifiers == KeyModifiers.Shift && _pointerHold)
            {
                vertex.X = vertex.OldX + (x - _lastPositionPointerPressed.X) / _zoom;
                vertex.Y = vertex.OldY + (y - _lastPositionPointerPressed.Y) / _zoom;
            }
        }
        private void MoveScene(double x, double y, KeyModifiers keyModifiers)
        {
            if (keyModifiers == KeyModifiers.Alt && _pointerHold)
            {
                _sceneOffsetPosition.X = (_lastPositionPointerPressed.X - x) / _zoom;
                _sceneOffsetPosition.Y = (_lastPositionPointerPressed.Y - y) / _zoom;
            }
        }

        private Point GetSceneDrawPosition(double x, double y)
        {
            var renderSize = Bounds.Size;
            return new Point(
                x * _zoom - (_scenePosition.X + _sceneOffsetPosition.X) * _zoom + (renderSize.Width / 2),
                y * _zoom - (_scenePosition.Y + _sceneOffsetPosition.Y) * _zoom + (renderSize.Height / 2));
        }

        private bool PointerInsideVertex(Vertex vertex, double x, double y)
        {
            var renderSize = Bounds.Size;
            double generalX = Math.Abs((x - renderSize.Width / 2) / _zoom + _scenePosition.X  - vertex.X);
            double generalY = Math.Abs((y - renderSize.Height / 2) / _zoom + _scenePosition.Y  - vertex.Y);

            double dist = Math.Sqrt(Math.Pow(generalX, 2) + Math.Pow(generalY, 2));
            return vertex.Size > dist;
        }
    }

    public struct ScenePoint
    {
        public double X;
        public double Y;
    }


    public class Vertex
    {
        public Vertex(double x, double y)
        {
            Edges = new ObservableCollection<Edge>();

            X = x;
            Y = y;
            OldX = x;
            OldY = y;
        }

        public ObservableCollection<Edge> Edges;

        public bool Selected;

        public readonly double Size = 10;
        public double X;
        public double Y;
        public double OldX;
        public double OldY;
    }

    public class Edge
    {
        public Vertex Vertex { get; set; }
        public Edge(Vertex vertex)
        {
            Vertex = vertex;
        }
    }
}
