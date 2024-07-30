using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using DynamicData;
using RouteOptimization.Utils;
using SkiaSharp;
using System;
using System.Collections;
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
        private IPen _penEdgeSelected;

        // Управление сценой
        private bool _pointerHold = false;
        private bool _oneVertexSelected = false;
        private Point _lastPositionPointerPressed;

        // Управление сценой
        double _zoom = 1;
        ScenePoint _scenePosition;
        ScenePoint _sceneOffsetPosition;

        public ObservableCollection<Vertex> Vertices { get; set; }
        public EdgeObservableCollection Edges { get; set; }

        public MapBuilderControl()
        {
            _brushBackground = new SolidColorBrush(Color.FromArgb(127, 25, 25, 50));
            _brushVertex = new SolidColorBrush(Color.FromArgb(255,100,100,200));
            _brushVertexSelected = new SolidColorBrush(Color.FromArgb(255, 200, 200, 255));

            _penEdge = new Pen(new SolidColorBrush(Color.FromArgb(255, 50,0,150)));
            _penEdgeSelected = new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 0, 255)));


            Vertices =
            [
                new Vertex(50,50),
                new Vertex(170,170),
                new Vertex(200,500),
            ];

            Edges = new EdgeObservableCollection();
            Edges.Add(Vertices[0], Vertices[1]);
            Edges.Add(Vertices[1], Vertices[2]);
            Edges.Add(Vertices[0], Vertices[2]);

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
                OnClickVertex(vertex, pointerPosition.X, pointerPosition.Y, e.KeyModifiers);
            }
            foreach (Edge edge in Edges.Reverse())
            {
                OnClickEdge(edge, pointerPosition.X, pointerPosition.Y, e.KeyModifiers);
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
                OnMoveVertex(vertex, pointerPosition.X, pointerPosition.Y, e.KeyModifiers);
            }
            OnMoveScene(pointerPosition.X, pointerPosition.Y, e.KeyModifiers);

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

            foreach (Edge edge in Edges)
            {
                DrawEdge(context, edge);
            }

            foreach (Vertex vertex in Vertices)
            {
                DrawVertex(context, vertex);
            }
        }

        private Point GetDrawPosition(double x, double y)
        {
            var renderSize = Bounds.Size;
            return new Point(
                x * _zoom - (_scenePosition.X + _sceneOffsetPosition.X) * _zoom + (renderSize.Width / 2),
                y * _zoom - (_scenePosition.Y + _sceneOffsetPosition.Y) * _zoom + (renderSize.Height / 2));
        }

        private void DrawEdge(DrawingContext context, Edge edge)
        {
            if (edge.Selected)
            {
                context.DrawLine(_penEdgeSelected, GetDrawPosition(edge.VertexFrom.X, edge.VertexFrom.Y), GetDrawPosition(edge.VertexTo.X, edge.VertexTo.Y));
            }
            else
            {
                context.DrawLine(_penEdge, GetDrawPosition(edge.VertexFrom.X, edge.VertexFrom.Y), GetDrawPosition(edge.VertexTo.X, edge.VertexTo.Y));
            }
        }
        private void DrawVertex(DrawingContext context, Vertex vertex)
        {
            if (vertex.Selected)
            {
                context.DrawEllipse(_brushVertexSelected, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * _zoom, vertex.Size * _zoom);
            }
            else
            {
                context.DrawEllipse(_brushVertex, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * _zoom, vertex.Size * _zoom);
            }
        }


        protected void OnClickVertex(Vertex vertex, double x, double y, KeyModifiers keyModifiers)
        {
            var renderSize = Bounds.Size;
            double cursorX = (x - renderSize.Width / 2) / _zoom + _scenePosition.X;
            double cursorY = (y - renderSize.Height / 2) / _zoom + _scenePosition.Y;

            if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorX, cursorY, vertex.Size) && _oneVertexSelected == false)
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

        protected void OnClickEdge(Edge edge, double x, double y, KeyModifiers keyModifiers)
        {
            var renderSize = Bounds.Size;
            double cursorX = (x - renderSize.Width / 2) / _zoom + _scenePosition.X;
            double cursorY = (y - renderSize.Height / 2) / _zoom + _scenePosition.Y;

            if (RouteMath.CursorInLine(edge.VertexFrom.X, edge.VertexFrom.Y, edge.VertexTo.X, edge.VertexTo.Y, cursorX, cursorY, 4) && _oneVertexSelected == false)
            {
                _oneVertexSelected = true;
                edge.Selected = true;
            }
            else
            {
                edge.Selected = false;
            }
        }

        protected void OnMoveVertex(Vertex vertex, double x, double y, KeyModifiers keyModifiers)
        {
            if (vertex.Selected && keyModifiers == KeyModifiers.Shift && _pointerHold)
            {
                vertex.X = vertex.OldX + (x - _lastPositionPointerPressed.X) / _zoom;
                vertex.Y = vertex.OldY + (y - _lastPositionPointerPressed.Y) / _zoom;
            }
        }
        protected void OnMoveScene(double x, double y, KeyModifiers keyModifiers)
        {
            if (keyModifiers == KeyModifiers.Alt && _pointerHold)
            {
                _sceneOffsetPosition.X = (_lastPositionPointerPressed.X - x) / _zoom;
                _sceneOffsetPosition.Y = (_lastPositionPointerPressed.Y - y) / _zoom;
            }
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

            X = x;
            Y = y;
            OldX = x;
            OldY = y;
        }

        public bool Selected;

        public readonly double Size = 10;
        public double X;
        public double Y;
        public double OldX;
        public double OldY;
    }

    public class Edge
    {
        public Vertex VertexFrom { get; set; }
        public Vertex VertexTo { get; set; }
        public bool Selected;
        public Edge(Vertex vertexTo, Vertex vertexFrom)
        {
            VertexTo = vertexTo;
            VertexFrom = vertexFrom;
        }
    }

    public class EdgeObservableCollection: IEnumerable<Edge>
    {
        public ObservableCollection<Edge> Edges { get; set; }
        public EdgeObservableCollection()
        {
            Edges = new ObservableCollection<Edge>();
        }

        public void Add(Vertex vertexTo, Vertex vertexFrom)
        {
            Edges.Add(new Edge(vertexTo, vertexFrom));
        }
        public void Remove(Vertex vertexTo, Vertex vertexFrom)
        {
            Edges.Remove(Get(vertexTo, vertexFrom));
        }
        public void Clear()
        {
            Edges.Clear();
        }
        public bool Contains(Vertex vertex)
        {
            return Edges.All(e => e.VertexTo == vertex) || 
                Edges.All(e => e.VertexFrom == vertex);
        }
        public bool Contains(Vertex vertexTo, Vertex vertexFrom)
        {
            return Edges.All(e => e.VertexTo == vertexTo && e.VertexFrom == vertexFrom) ||
                Edges.All(e => e.VertexTo == vertexFrom && e.VertexFrom == vertexTo);
        }
        public Edge Get(Vertex vertexTo, Vertex vertexFrom)
        {
            return Edges.FirstOrDefault(v => v.VertexTo == vertexTo && v.VertexFrom == vertexFrom) ?? 
                Edges.First(v => v.VertexTo == vertexFrom && v.VertexFrom == vertexTo);
        }

        public IEnumerator<Edge> GetEnumerator()
        {
            return Edges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Edges.GetEnumerator();
        }
    }
}
