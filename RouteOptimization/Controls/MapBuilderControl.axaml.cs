using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
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
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RouteOptimization.Controls
{
    public partial class MapBuilderControl : Control
    {
        private IPen _penEdge;
        private IPen _penEdgeFocused;
        private IPen _penGridScene;

        private double _pointerLastPressX;
        private double _pointerLastPressY;

        private Scene _scene = new Scene(0, 0);
        private Vertex? _selectedVertex;
        private Vertex? _focusedVertex;
        private Edge? _focusedEdge;
        private IEnumerable<Vertex> _vertices;
        private IEnumerable<Edge> _edges;


        #region Background Property
        public static readonly StyledProperty<IBrush?> BackgroundProperty =
            AvaloniaProperty.Register<MapBuilderControl, IBrush?>(nameof(Background), new SolidColorBrush(Color.FromArgb(127, 25, 25, 50)));
        public IBrush? Background
        {
            get { return GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        #endregion

        #region Vertex Brush Property
        public static readonly StyledProperty<IBrush?> VertexBrushProperty =
            AvaloniaProperty.Register<MapBuilderControl, IBrush?>(nameof(VertexBrush), new SolidColorBrush(Color.FromArgb(255, 95, 95, 207)));
        public IBrush? VertexBrush
        {
            get { return GetValue(VertexBrushProperty); }
            set { SetValue(VertexBrushProperty, value); }
        }
        #endregion

        #region Selected Vertex Brush Property
        public static readonly StyledProperty<IBrush?> SelectedVertexBrushProperty =
            AvaloniaProperty.Register<MapBuilderControl, IBrush?>(nameof(SelectedVertexBrush), new SolidColorBrush(Color.FromArgb(255, 191, 191, 255)));
        public IBrush? SelectedVertexBrush
        {
            get { return GetValue(SelectedVertexBrushProperty); }
            set { SetValue(SelectedVertexBrushProperty, value); }
        }
        #endregion

        # region Focused Vertex Brush Property
        public static readonly StyledProperty<IBrush?> FocusedVertexBrushProperty =
            AvaloniaProperty.Register<MapBuilderControl, IBrush?>(nameof(FocusedVertexBrush), new SolidColorBrush(Color.FromArgb(255, 127, 127, 239)));
        public IBrush? FocusedVertexBrush
        {
            get { return GetValue(FocusedVertexBrushProperty); }
            set { SetValue(FocusedVertexBrushProperty, value); }
        }
        #endregion

        # region Edge Brush Property
        public static readonly StyledProperty<IBrush?> EdgeBrushProperty =
            AvaloniaProperty.Register<MapBuilderControl, IBrush?>(nameof(EdgeBrush), new SolidColorBrush(Color.FromArgb(255, 64, 0, 159)));
        public IBrush? EdgeBrush
        {
            get { return GetValue(EdgeBrushProperty); }
            set { SetValue(EdgeBrushProperty, value); }
        }
        #endregion

        #region Focused Edge Brush Property
        public static readonly StyledProperty<IBrush?> FocusedEdgeBrushProperty =
            AvaloniaProperty.Register<MapBuilderControl, IBrush?>(nameof(FocusedEdgeBrush), new SolidColorBrush(Color.FromArgb(255, 80, 0, 191)));
        public IBrush? FocusedEdgeBrush
        {
            get { return GetValue(FocusedEdgeBrushProperty); }
            set { SetValue(FocusedEdgeBrushProperty, value); }
        }
        #endregion

        #region Grid Brush Property
        public static readonly StyledProperty<IBrush?> GridBrushProperty =
            AvaloniaProperty.Register<MapBuilderControl, IBrush?>(nameof(GridBrush), new SolidColorBrush(Color.FromArgb(255, 25, 25, 50)));
        public IBrush? GridBrush
        {
            get { return GetValue(GridBrushProperty); }
            set { SetValue(GridBrushProperty, value); }
        }
        #endregion

        public static readonly StyledProperty<ICommand?> MoveCommandProperty =
            AvaloniaProperty.Register<MapBuilderControl, ICommand?>(nameof(MoveCommand), default);
        public ICommand? MoveCommand
        {
            get { return GetValue(MoveCommandProperty); }
            set { SetValue(MoveCommandProperty, value); }
        }


        #region Scene Property
        public static readonly DirectProperty<MapBuilderControl, Scene> SceneProperty =
            AvaloniaProperty.RegisterDirect<MapBuilderControl, Scene>(
                nameof(Scene),
                o => o._scene,
                (o, v) => o._scene = v);
        public Scene Scene
        {
            get { return _scene; }
            set { SetAndRaise(SceneProperty, ref _scene, value); }
        }
        #endregion

        #region Selected Vertex Property
        public static readonly DirectProperty<MapBuilderControl, Vertex?> SelectedVertexProperty =
            AvaloniaProperty.RegisterDirect<MapBuilderControl, Vertex?>(
                nameof(SelectedVertex),
                o => o._selectedVertex,
                (o, v) => o._selectedVertex = v);
        public Vertex? SelectedVertex
        {
            get { return _selectedVertex; }
            set { SetAndRaise(SelectedVertexProperty, ref _selectedVertex, value); }
        }
        #endregion

        #region Edges Property
        public static readonly DirectProperty<MapBuilderControl, IEnumerable<Edge>> EdgesProperty =
            AvaloniaProperty.RegisterDirect<MapBuilderControl, IEnumerable<Edge>>(
                nameof(Edges),
                o => o._edges,
                (o, v) => o._edges = v);
        public IEnumerable<Edge> Edges
        {
            get { return _edges; }
            set { SetAndRaise(EdgesProperty, ref _edges, value); }
        }
        #endregion

        #region Vertices Property
        public static readonly DirectProperty<MapBuilderControl, IEnumerable<Vertex>> VerticesProperty = 
            AvaloniaProperty.RegisterDirect<MapBuilderControl, IEnumerable<Vertex>>(
                nameof(Vertices), 
                o => o._vertices, 
                (o, v) => o._vertices = v);
        public IEnumerable<Vertex> Vertices
        {
            get { return _vertices; }
            set { 
                SetAndRaise(VerticesProperty, ref _vertices, value); 
            }
        }
        #endregion

        public MapBuilderControl()
        {

            _penEdge = new Pen(EdgeBrush);
            _penEdgeFocused = new Pen(FocusedEdgeBrush);
            _penGridScene = new Pen(GridBrush);

            _vertices = new List<Vertex>();
            _edges = new List<Edge>();

            InitializeComponent();
        }


        private void OnVerticesPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            InvalidateVisual();
        }



        #region Override control methods
        public sealed override void Render(DrawingContext context)
        {
            if (Background != null)
            {
                var renderSize = Bounds.Size;
                context.FillRectangle(Background, new Rect(renderSize));
            }

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

            base.Render(context);
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            InvalidateVisual();
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            var cursorPosition = e.GetPosition(this);
            _pointerLastPressX = cursorPosition.X;
            _pointerLastPressY = cursorPosition.Y;

            if (e.Pointer.Type == PointerType.Mouse)
            {
                var cursorProperties = e.GetCurrentPoint(this).Properties;
                if (cursorProperties.IsLeftButtonPressed)
                {
                    SelectedVertex = null;

                    foreach (Vertex vertex in _vertices.Reverse())
                    {
                        var renderSize = Bounds.Size;
                        double cursorX = (cursorPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                        double cursorY = (cursorPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;

                        if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorX, cursorY, vertex.Size))
                        {
                            SelectedVertex = vertex;
                            Vertex.PerformPress(vertex);

                            goto draw;
                        }
                    }
                    //if (!isSelectedVertex)
                    //{
                    //    _scene.PerformPress();
                    //}
                }
            }
            draw: InvalidateVisual();
        }
        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {

            var cursorPosition = e.GetPosition(this);
            if (e.Pointer.Type == PointerType.Mouse)
            {
                var cursorProperties = e.GetCurrentPoint(this).Properties;
                if (!cursorProperties.IsLeftButtonPressed)
                {
                    if (SelectedVertex != null)
                    {
                        if (MoveCommand?.CanExecute(SelectedVertex) ?? false)
                        {
                            MoveCommand.Execute(SelectedVertex);
                        }
                    }
                    Scene.PerformRelease(_scene);

                }
            }
            InvalidateVisual();
        }
        protected override void OnPointerMoved(PointerEventArgs e)
        {
            var cursorPosition = e.GetPosition(this);
            if (e.Pointer.Type == PointerType.Mouse)
            {
                var cursorProperties = e.GetCurrentPoint(this).Properties;
                if (cursorProperties.IsLeftButtonPressed)
                {
                    if (_selectedVertex != null)
                    {
                        Vertex.PerformMove(_selectedVertex, new Point(
                            (cursorPosition.X - _pointerLastPressX) / _scene.Zoom,
                            (cursorPosition.Y - _pointerLastPressY) / _scene.Zoom
                            ));

                        goto draw;
                    }
                    else
                    {
                        Scene.PerformMove(_scene, new Point(
                                (_pointerLastPressX - cursorPosition.X) / _scene.Zoom,
                                (_pointerLastPressY - cursorPosition.Y) / _scene.Zoom
                                ));

                        goto draw;
                    }
                }

                // Фокусирование
                _focusedEdge = null;
                _focusedVertex = null;
                foreach (Vertex vertex in _vertices.Reverse())
                {
                    var renderSize = Bounds.Size;
                    double cursorX = (cursorPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                    double cursorY = (cursorPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;
                
                    if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorX, cursorY, vertex.Size))
                    {
                        _focusedVertex = vertex;

                        goto draw;
                    }
                }
                foreach (Edge edge in _edges.Reverse())
                {
                    var renderSize = Bounds.Size;
                    double cursorX = (cursorPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                    double cursorY = (cursorPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;
                
                    if (RouteMath.CursorInLine(edge.VertexFrom.X, edge.VertexFrom.Y, edge.VertexTo.X, edge.VertexTo.Y, cursorX, cursorY, 4))
                    {
                        _focusedEdge = edge;

                        goto draw;
                    }
                }
            }
            draw: InvalidateVisual();
        }
        protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
        {
            Scene.PerformWheelChanged(_scene, e.Delta);
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
            if (_focusedEdge == edge)
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
            if (_selectedVertex == vertex)
            {
                context.DrawEllipse(SelectedVertexBrush, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
            }
            else if (_focusedVertex == vertex)
            {
                context.DrawEllipse(FocusedVertexBrush, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
            }
            else
            {
                context.DrawEllipse(VertexBrush, null, GetDrawPosition(vertex.X, vertex.Y), vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
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
