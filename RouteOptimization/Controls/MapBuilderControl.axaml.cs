using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using DynamicData;
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
        private IBrush _brushVertexFocused;

        private IPen _penEdge;
        private IPen _penEdgeSelected;
        private IPen _penEdgeFocused;

        private IPen _penGridScene;

        private double _pointerLastPressX;
        private double _pointerLastPressY;

        private Scene _scene = new Scene(0, 0);
        private IEnumerable<Vertex> _vertices = new List<Vertex>();
        private IEnumerable<Edge> _edges = new List<Edge>();

        #region SceneSource property
        public Scene SceneSource
        {
            get { return _scene; }
            set { SetAndRaise(SceneSourceProperty, ref _scene, value); }
        }
        public static readonly DirectProperty<MapBuilderControl, Scene> SceneSourceProperty = AvaloniaProperty.RegisterDirect<MapBuilderControl, Scene>("SceneSource", o => o._scene, (o, v) => o._scene = v);
        private static void OnSceneSourcePropertyChanged(MapBuilderControl sender, AvaloniaPropertyChangedEventArgs e)
        {
            sender.OnScenePropertyChanged((Scene?)e.OldValue, (Scene?)e.NewValue);
        }
        private void OnScenePropertyChanged(Scene? oldValue, Scene? newValue)
        {
            var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;
            if (oldValueINotifyCollectionChanged != null)
                oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(OnEdgesPropertyHandler);
            var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (newValueINotifyCollectionChanged != null)
                newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(OnEdgesPropertyHandler);
        }
        private void OnSceneSourcePropertyHandler(object? sender, NotifyCollectionChangedEventArgs e)
        {
            InvalidateVisual();
        }
        #endregion

        #region EdgesSource property
        public IEnumerable<Edge> EdgesSource
        {
            get { return _edges; }
            set { SetAndRaise(EdgesSourceProperty, ref _edges, value); }
        }
        public static readonly DirectProperty<MapBuilderControl, IEnumerable<Edge>> EdgesSourceProperty = AvaloniaProperty.RegisterDirect<MapBuilderControl, IEnumerable<Edge>>("EdgesSource", o => o._edges, (o, v) => o._edges = v);
        private static void OnEdgesSourcePropertyChanged(MapBuilderControl sender, AvaloniaPropertyChangedEventArgs e)
        {
            sender.OnEdgesPropertyChanged((IEnumerable?)e.OldValue, (IEnumerable?)e.NewValue);
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
        private void OnEdgesPropertyHandler(object? sender, NotifyCollectionChangedEventArgs e)
        {
            InvalidateVisual();
        }
        #endregion

        #region VerticesSource property
        public IEnumerable<Vertex> VerticesSource
        {
            get { return _vertices; }
            set { SetAndRaise(VerticesSourceProperty, ref _vertices, value); }
        }
        public static readonly DirectProperty<MapBuilderControl, IEnumerable<Vertex>> VerticesSourceProperty = AvaloniaProperty.RegisterDirect<MapBuilderControl, IEnumerable<Vertex>>("VerticesSource", o => o._vertices, (o, v) => o._vertices = v);
        private static void OnVerticesSourcePropertyChanged(MapBuilderControl sender, AvaloniaPropertyChangedEventArgs e)
        {
            sender.OnVerticesPropertyChanged((IEnumerable?)e.OldValue, (IEnumerable?)e.NewValue);
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
        private void OnVerticesPropertyHandler(object? sender, NotifyCollectionChangedEventArgs e)
        {
            InvalidateVisual();
        }
        #endregion

        public MapBuilderControl()
        {
            SceneSourceProperty.Changed.AddClassHandler<MapBuilderControl>(OnSceneSourcePropertyChanged);
            VerticesSourceProperty.Changed.AddClassHandler<MapBuilderControl>(OnVerticesSourcePropertyChanged);
            EdgesSourceProperty.Changed.AddClassHandler<MapBuilderControl>(OnEdgesSourcePropertyChanged);

            _brushBackground = new SolidColorBrush(Color.FromArgb(127, 25, 25, 50));

            _brushVertex = new SolidColorBrush(Color.FromArgb(255,95,95,207));
            _brushVertexFocused = new SolidColorBrush(Color.FromArgb(255, 127, 127, 239));
            _brushVertexSelected = new SolidColorBrush(Color.FromArgb(255, 191, 191, 255));

            _penEdge = new Pen(new SolidColorBrush(Color.FromArgb(255, 64,0,159)));
            _penEdgeFocused = new Pen(new SolidColorBrush(Color.FromArgb(255, 80, 0, 191)));
            _penEdgeSelected = new Pen(new SolidColorBrush(Color.FromArgb(255, 112, 0, 255)));

            _penGridScene = new Pen(new SolidColorBrush(Color.FromArgb(255, 25, 25, 50)));

            InitializeComponent();
        }

        #region Override control methods
        public override void Render(DrawingContext context)
        {
            base.Render(context);

            if (_brushBackground != null)
            {
                var renderSize = Bounds.Size;
                context.FillRectangle(_brushBackground, new Rect(renderSize));
            };

            if (_scene.Zoom < 0.01)
            {
                DrawGrid(context, 5000);
            }
            else if (_scene.Zoom <= 0.10)
            {
                DrawGrid(context, 500);
            }
            else if (_scene.Zoom <= 0.25)
            {
                DrawGrid(context, 200);
            }
            else if (_scene.Zoom <= 0.50)
            {
                DrawGrid(context, 100);
            }
            else if (_scene.Zoom <= 5.0)
            {
                DrawGrid(context, 50);
            }
            else if (_scene.Zoom > 5.00)
            {
                DrawGrid(context, 5);
            }

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
            var pointerPosition = e.GetPosition(this);
            _pointerLastPressX = pointerPosition.X;
            _pointerLastPressY = pointerPosition.Y;

            if (e.Pointer.Type == PointerType.Mouse)
            {
                var properties = e.GetCurrentPoint(this).Properties;
                if (properties.IsLeftButtonPressed)
                {
                    bool oneVertexSelected = false;

                    foreach (Vertex vertex in _vertices.Reverse())
                    {
                        var renderSize = Bounds.Size;
                        double cursorX = (pointerPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                        double cursorY = (pointerPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;

                        if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorX, cursorY, vertex.Size) && oneVertexSelected == false)
                        {
                            oneVertexSelected = true;
                            vertex.PerformPress();
                        }
                        else
                        {
                            vertex.PerformRelease();
                        }
                    }
                    foreach (Edge edge in _edges.Reverse())
                    {
                        var renderSize = Bounds.Size;
                        double cursorX = (pointerPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                        double cursorY = (pointerPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;

                        if (RouteMath.CursorInLine(edge.VertexFrom.X, edge.VertexFrom.Y, edge.VertexTo.X, edge.VertexTo.Y, cursorX, cursorY, 4) && oneVertexSelected == false)
                        {
                            oneVertexSelected = true;
                            edge.PerformPress();
                        }
                        else
                        {
                            edge.PerformRelease();
                        }
                    }
                    if (!oneVertexSelected)
                    {
                        _scene.PerformPress();
                    }

                }
            }
            InvalidateVisual();
        }
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            var pointerPosition = e.GetPosition(this);
            if (e.Pointer.Type == PointerType.Mouse)
            {
                var properties = e.GetCurrentPoint(this).Properties;
                if (!properties.IsLeftButtonPressed)
                {
                    _scene.PerformRelease();
                }
            }
        }
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            var pointerPosition = e.GetPosition(this);
            if (e.Pointer.Type == PointerType.Mouse)
            {
                bool oneVertexSelected = false;

                var properties = e.GetCurrentPoint(this).Properties;
                if (properties.IsLeftButtonPressed)
                {
                    foreach (Vertex vertex in _vertices)
                    {
                        if (vertex.Selected)
                        {
                            oneVertexSelected = true;
                            vertex.PerformMove(new Point(
                                (pointerPosition.X - _pointerLastPressX) / _scene.Zoom,
                                (pointerPosition.Y - _pointerLastPressY) / _scene.Zoom
                                ));
                        }
                    }
                    if (!oneVertexSelected)
                    {
                        _scene.PerformMove(new Point(
                                (_pointerLastPressX - pointerPosition.X) / _scene.Zoom,
                                (_pointerLastPressY - pointerPosition.Y) / _scene.Zoom
                                ));
                    }
                }

                if (!oneVertexSelected)
                {
                    bool oneFocused = false;
                    foreach (Vertex vertex in _vertices.Reverse())
                    {
                        var renderSize = Bounds.Size;
                        double cursorX = (pointerPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                        double cursorY = (pointerPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;

                        if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorX, cursorY, vertex.Size) && oneFocused == false)
                        {
                            oneFocused = true;
                            vertex.PerformEnter();
                        }
                        else
                        {
                            vertex.PerformExit();
                        }
                    }
                    foreach (Edge edge in _edges.Reverse())
                    {
                        var renderSize = Bounds.Size;
                        double cursorX = (pointerPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                        double cursorY = (pointerPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;

                        if (RouteMath.CursorInLine(edge.VertexFrom.X, edge.VertexFrom.Y, edge.VertexTo.X, edge.VertexTo.Y, cursorX, cursorY, 4) && oneFocused == false)
                        {
                            oneFocused = true;
                            edge.PerformEnter();
                        }
                        else
                        {
                            edge.PerformExit();
                        }
                    }
                }
            }
            InvalidateVisual();
        }
        protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
        {
            _scene.PerformWheelChanged(e.Delta);
            InvalidateVisual();
        }
        #endregion

        private Point GetDrawPosition(double x, double y)
        {
            var window = Bounds.Size;
            return new Point(
                x * _scene.Zoom - _scene.X  * _scene.Zoom + (window.Width / 2),
                y * _scene.Zoom - _scene.Y * _scene.Zoom + (window.Height / 2));
        }

        private void DrawEdge(DrawingContext context, Edge edge)
        {
            if (edge.Selected)
            {
                context.DrawLine(_penEdgeSelected, GetDrawPosition(edge.VertexFrom.X, edge.VertexFrom.Y), GetDrawPosition(edge.VertexTo.X, edge.VertexTo.Y));
            }
            else if (edge.Focused)
            {
                context.DrawLine(_penEdgeFocused, GetDrawPosition(edge.VertexFrom.X, edge.VertexFrom.Y), GetDrawPosition(edge.VertexTo.X, edge.VertexTo.Y));
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
                context.DrawEllipse(_brushVertexSelected, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
            }
            else if (vertex.Focused)
            {
                context.DrawEllipse(_brushVertexFocused, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
            }
            else
            {
                context.DrawEllipse(_brushVertex, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
            }
        }
        private void DrawGrid(DrawingContext context, double gridSize)
        {
            var window = Bounds.Size;

            double step = gridSize * _scene.Zoom;
            double x_start = (step + window.Width / 2 - _scene.X * _scene.Zoom) % step;
            double x = x_start;
            double y = (step + window.Height / 2 - _scene.Y * _scene.Zoom) % step;

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
