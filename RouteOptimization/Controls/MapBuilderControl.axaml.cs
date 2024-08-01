using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using DynamicData;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RouteOptimization.Controls.MapBuilder;
using RouteOptimization.Utils;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Reflection;

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

        private IPen _penGridScene;

        private double _sceneOffsetX;
        private double _sceneOffsetY;

        private bool _vertexOneSelected;

        private bool _pointerHold;
        private double _pointerLastPressX;
        private double _pointerLastPressY;

        public SceneEntity Scene { get; }

        private IEnumerable<Vertex> _vertices = new List<Vertex>();
        private IEnumerable<Edge> _edges = new List<Edge>();

        public IEnumerable<Vertex> VerticesSource
        {
            get { return _vertices; }
            set { SetAndRaise(VerticesSourceProperty, ref _vertices, value); }
        }
        public IEnumerable<Edge> EdgesSource
        {
            get { return _edges; }
            set { SetAndRaise(EdgesSourceProperty, ref _edges, value); }
        }

        public static readonly DirectProperty<MapBuilderControl, IEnumerable<Vertex>> VerticesSourceProperty = AvaloniaProperty.RegisterDirect<MapBuilderControl, IEnumerable<Vertex>> ("VerticesSource", o => o._vertices, (o,v) => o._vertices = v);
        public static readonly DirectProperty<MapBuilderControl, IEnumerable<Edge>> EdgesSourceProperty = AvaloniaProperty.RegisterDirect<MapBuilderControl, IEnumerable<Edge>>("EdgesSource", o => o._edges, (o, v) => o._edges = v);
        private static void OnVerticesSourcePropertyChanged(MapBuilderControl sender, AvaloniaPropertyChangedEventArgs e)
        {
            sender.OnVerticesPropertyChanged((IEnumerable?)e.OldValue, (IEnumerable?)e.NewValue);
        }
        private static void OnEdgesSourcePropertyChanged(MapBuilderControl sender, AvaloniaPropertyChangedEventArgs e)
        {
            sender.OnEdgesPropertyChanged((IEnumerable?)e.OldValue, (IEnumerable?)e.NewValue);
        }
        private void OnVerticesPropertyChanged(IEnumerable? oldValue, IEnumerable? newValue)
        {
            var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;
            if (oldValueINotifyCollectionChanged != null)
                oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnVerticesPropertyHandler);
            var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (newValueINotifyCollectionChanged != null)
                newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(OnVerticesPropertyHandler);
        }
        private void OnEdgesPropertyChanged(IEnumerable? oldValue, IEnumerable? newValue)
        {
            var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;
            if (oldValueINotifyCollectionChanged != null)
                oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnEdgesPropertyHandler);
            var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (newValueINotifyCollectionChanged != null)
                newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(OnEdgesPropertyHandler);
        }
        private void OnVerticesPropertyHandler(object? sender, NotifyCollectionChangedEventArgs e)
        {
            InvalidateVisual();
        }
        private void OnEdgesPropertyHandler(object? sender, NotifyCollectionChangedEventArgs e)
        {
            InvalidateVisual();
        }

        public MapBuilderControl()
        {
            VerticesSourceProperty.Changed.AddClassHandler<MapBuilderControl>(OnVerticesSourcePropertyChanged);
            EdgesSourceProperty.Changed.AddClassHandler<MapBuilderControl>(OnEdgesSourcePropertyChanged);

            _brushBackground = new SolidColorBrush(Color.FromArgb(127, 25, 25, 50));
            _brushVertex = new SolidColorBrush(Color.FromArgb(255,100,100,200));
            _brushVertexSelected = new SolidColorBrush(Color.FromArgb(255, 200, 200, 255));

            _penEdge = new Pen(new SolidColorBrush(Color.FromArgb(255, 50,0,150)));
            _penEdgeSelected = new Pen(new SolidColorBrush(Color.FromArgb(255, 100, 0, 255)));

            _penGridScene = new Pen(new SolidColorBrush(Color.FromArgb(255, 25, 25, 50)));

            Scene = new SceneEntity(0,0);

            InitializeComponent();
        }

        #region Override control methods
        public override void Render(DrawingContext context)
        {
            if (_brushBackground != null)
            {
                var renderSize = Bounds.Size;
                context.FillRectangle(_brushBackground, new Rect(renderSize));
            };
            var ft = new FormattedText(
                    "Координаты X=" + Math.Round(Scene.X, 3) +
                    " / Y=" + Math.Round(Scene.Y, 3) +
                    "Масштаб: " + Math.Round(Scene.Zoom, 2) * 100 + "%",
                    new CultureInfo(1),
                    FlowDirection.LeftToRight,
                    Typeface.Default,
                    14,
                    Brushes.White);

            if (Scene.Zoom < 0.01)
            {
                DrawGrid(context, 5000);
            }
            else if (Scene.Zoom <= 0.10)
            {
                DrawGrid(context, 500);
            }
            else if (Scene.Zoom <= 0.25)
            {
                DrawGrid(context, 200);
            }
            else if (Scene.Zoom <= 0.50)
            {
                DrawGrid(context, 100);
            }
            else if (Scene.Zoom <= 5.0)
            {
                DrawGrid(context, 50);
            }
            else if (Scene.Zoom > 5.00)
            {
                DrawGrid(context, 5);
            }

            context.DrawText(ft, new Point(0, 0));

            foreach (Edge edge in _edges)
            {
                DrawEdge(context, edge);
            }

            foreach (Vertex vertex in _vertices)
            {
                DrawVertex(context, vertex);
            }
        }
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            Point pointerPosition = e.GetPosition(this);

            _vertexOneSelected = false;
            _pointerLastPressX = pointerPosition.X;
            _pointerLastPressY = pointerPosition.Y;
            _pointerHold = true;

            foreach (Vertex vertex in _vertices.Reverse())
            {
                OnVertexClicked(vertex, pointerPosition.X, pointerPosition.Y, e.KeyModifiers);
            }
            foreach (Edge edge in _edges.Reverse())
            {
                OnEdgeClicked(edge, pointerPosition.X, pointerPosition.Y, e.KeyModifiers);
            }


            InvalidateVisual();
        }
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            Scene.X += _sceneOffsetX;
            Scene.Y += _sceneOffsetY;

            _pointerHold = false;

            _sceneOffsetX = 0;
            _sceneOffsetY = 0;

            foreach (Vertex vertex in _vertices)
            {
                vertex.LastX = 0;
                vertex.LastY = 0;
            }
        }
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            Point pointerPosition = e.GetPosition(this);
            foreach (Vertex vertex in _vertices)
            {
                OnVertexMoved(vertex, pointerPosition.X, pointerPosition.Y, e.KeyModifiers);
            }
            OnSceneMoved(pointerPosition.X, pointerPosition.Y, e.KeyModifiers);

            InvalidateVisual();
        }
        protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
        {
            if (e.Delta.Y > 0)
            {
                if (Scene.Zoom <= 100)
                    Scene.Zoom *= 1.10;
            }
            else
            {
                if (Scene.Zoom >= 0.05)
                    Scene.Zoom /= 1.10;
            }
            InvalidateVisual();
        }
        #endregion

        #region Virtual control methods
        protected virtual void OnVertexClicked(Vertex vertex, double x, double y, KeyModifiers keyModifiers)
        {
            vertex.LastX = vertex.X;
            vertex.LastY = vertex.Y;

            var renderSize = Bounds.Size;
            double cursorX = (x - renderSize.Width / 2) / Scene.Zoom + Scene.X;
            double cursorY = (y - renderSize.Height / 2) / Scene.Zoom + Scene.Y;

            if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorX, cursorY, vertex.Size) && _vertexOneSelected == false)
            {
                _vertexOneSelected = true;
                vertex.Selected = true;
            }
            else
            {
                if (keyModifiers != KeyModifiers.Shift &&
                    keyModifiers != KeyModifiers.Control)
                    vertex.Selected = false;
            }
        }
        protected virtual void OnEdgeClicked(Edge edge, double x, double y, KeyModifiers keyModifiers)
        {
            var renderSize = Bounds.Size;
            double cursorX = (x - renderSize.Width / 2) / Scene.Zoom + Scene.X;
            double cursorY = (y - renderSize.Height / 2) / Scene.Zoom + Scene.Y;

            if (RouteMath.CursorInLine(edge.VertexFrom.X, edge.VertexFrom.Y, edge.VertexTo.X, edge.VertexTo.Y, cursorX, cursorY, 4) && _vertexOneSelected == false)
            {
                _vertexOneSelected = true;
                edge.Selected = true;
            }
            else
            {
                edge.Selected = false;
            }
        }
        protected virtual void OnVertexMoved(Vertex vertex, double x, double y, KeyModifiers keyModifiers)
        {
            if (vertex.Selected && keyModifiers == KeyModifiers.Shift && _pointerHold)
            {
                vertex.X = vertex.LastX + (x - _pointerLastPressX) / Scene.Zoom;
                vertex.Y = vertex.LastY + (y - _pointerLastPressY) / Scene.Zoom;
            }
        }
        protected virtual void OnSceneMoved(double x, double y, KeyModifiers keyModifiers)
        {
            if (keyModifiers == KeyModifiers.Alt && _pointerHold)
            {
                _sceneOffsetX = (_pointerLastPressX - x) / Scene.Zoom;
                _sceneOffsetY = (_pointerLastPressY - y) / Scene.Zoom;
            }
        }
        #endregion

        private Point GetDrawPosition(double x, double y)
        {
            var window = Bounds.Size;
            return new Point(
                x * Scene.Zoom - (Scene.X + _sceneOffsetX) * Scene.Zoom + (window.Width / 2),
                y * Scene.Zoom - (Scene.Y + _sceneOffsetY) * Scene.Zoom + (window.Height / 2));
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
                context.DrawEllipse(_brushVertexSelected, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * Scene.Zoom, vertex.Size * Scene.Zoom);
            }
            else
            {
                context.DrawEllipse(_brushVertex, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * Scene.Zoom, vertex.Size * Scene.Zoom);
            }
        }
        private void DrawGrid(DrawingContext context, double gridSize)
        {
            var window = Bounds.Size;

            double step = gridSize * Scene.Zoom;
            double x_start = (step + window.Width / 2 - (Scene.X + _sceneOffsetX) * Scene.Zoom) % step;
            double x = x_start;
            double y = (step + window.Height / 2 - (Scene.Y + _sceneOffsetY) * Scene.Zoom) % step;

            while (y < window.Height)
            {
                context.DrawLine(_penGridScene, new Point(x, 0), new Point(x, window.Height));
                x += step;

                if (x >= window.Width)
                {
                    x = x_start;
                    y += step;
                }

                context.DrawLine(_penGridScene, new Point(0, y), new Point(window.Width, y));
            }
        }
    }
}
