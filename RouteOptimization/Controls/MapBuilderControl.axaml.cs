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
using RouteOptimization.Models;
using Location = RouteOptimization.Models.Location;

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
        private Location? _selectedVertex;
        private Location? _focusedVertex;
        private Route? _focusedEdge;
        private IEnumerable<Location> _vertices;
        private IEnumerable<Route> _edges;

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

        #region Command Property
        public static readonly StyledProperty<ICommand?> MoveCommandProperty =
            AvaloniaProperty.Register<MapBuilderControl, ICommand?>(nameof(MoveCommand), default);
        public ICommand? MoveCommand
        {
            get { return GetValue(MoveCommandProperty); }
            set { SetValue(MoveCommandProperty, value); }
        }
        #endregion

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
        public static readonly DirectProperty<MapBuilderControl, Location?> SelectedVertexProperty =
            AvaloniaProperty.RegisterDirect<MapBuilderControl, Location?>(
                nameof(SelectedVertex),
                o => o._selectedVertex,
                (o, v) => o._selectedVertex = v);
        public Location? SelectedVertex
        {
            get { return _selectedVertex; }
            set { SetAndRaise(SelectedVertexProperty, ref _selectedVertex, value); }
        }
        #endregion

        #region Edges Property
        public static readonly DirectProperty<MapBuilderControl, IEnumerable<Route>> EdgesProperty =
            AvaloniaProperty.RegisterDirect<MapBuilderControl, IEnumerable<Route>>(
                nameof(Edges),
                o => o._edges,
                (o, v) => o._edges = v);
        public IEnumerable<Route> Edges
        {
            get { return _edges; }
            set { SetAndRaise(EdgesProperty, ref _edges, value); }
        }
        #endregion

        #region Vertices Property
        public static readonly DirectProperty<MapBuilderControl, IEnumerable<Location>> VerticesProperty = 
            AvaloniaProperty.RegisterDirect<MapBuilderControl, IEnumerable<Location>>(
                nameof(Vertices), 
                o => o._vertices, 
                (o, v) => o._vertices = v);
        public IEnumerable<Location> Vertices
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

            _vertices = new List<Location>();
            _edges = new List<Route>();

            InitializeComponent();
        }

        #region Override control methods
        public sealed override void Render(DrawingContext context)
        {
            if (Background != null)
            {
                var renderSize = Bounds.Size;
                context.FillRectangle(Background, new Rect(renderSize));
            }

            DrawGrid(context, _scene.GridSize);

            foreach (IEdge edge in _edges)
            {
                DrawEdge(context, edge);
            }

            foreach (IVertex vertex in _vertices)
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

                    foreach (Location vertex in _vertices.Reverse())
                    {
                        var renderSize = Bounds.Size;
                        double cursorX = (cursorPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                        double cursorY = (cursorPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;

                        if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorX, cursorY, vertex.Size))
                        {
                            SelectedVertex = vertex;
                            VertexUI.PerformPress(vertex);

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
                        VertexUI.PerformMove(_selectedVertex, new Point(
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
                foreach (Location vertex in _vertices.Reverse())
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
                foreach (Route edge in _edges.Reverse())
                {
                    var renderSize = Bounds.Size;
                    double cursorX = (cursorPosition.X - renderSize.Width / 2) / _scene.Zoom + _scene.X;
                    double cursorY = (cursorPosition.Y - renderSize.Height / 2) / _scene.Zoom + _scene.Y;
                
                    if (RouteMath.CursorInLine(edge.StartX, edge.StartY, edge.FinishX, edge.FinishY, cursorX, cursorY, 4))
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

        /// <summary>
        /// Получить положение точки в ввиде структуры Avalonia.Point
        /// </summary>
        /// <param name="x">Координаты по X</param>
        /// <param name="y">Координаты по Y</param>
        /// <returns>Возвращает Avalonia.Point</returns>
        private Point GetPointPosition(double x, double y)
        {
            var window = Bounds.Size;
            return new Point(
                x * _scene.Zoom - _scene.X  * _scene.Zoom + (window.Width / 2),
                y * _scene.Zoom - _scene.Y * _scene.Zoom + (window.Height / 2));
        }

        /// <summary>
        /// Отрисовать ребро между первой и второй точкой
        /// </summary>
        /// <param name="context">Контекст отрисовки</param>
        /// <param name="edge">Объект ребра</param>
        private void DrawEdge(DrawingContext context, IEdge edge)
        {
            Point startPoint = GetPointPosition(edge.StartX, edge.StartY);
            Point finishPoint = GetPointPosition(edge.FinishX, edge.FinishY);
            if (_focusedEdge == edge)
            {
                context.DrawLine(_penEdgeFocused, startPoint, finishPoint);
            }
            else
            {
                context.DrawLine(_penEdge, startPoint, finishPoint);
            }
        }
        /// <summary>
        /// Отрисовать точку
        /// </summary>
        /// <param name="context">Контекст отрисовки</param>
        /// <param name="vertex">Объект точки</param>
        private void DrawVertex(DrawingContext context, IVertex vertex)
        {
            Point point = GetPointPosition(vertex.X, vertex.Y);
            if (_selectedVertex == vertex)
            {
                context.DrawEllipse(SelectedVertexBrush, null, point, vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
            }
            else if (_focusedVertex == vertex)
            {
                context.DrawEllipse(FocusedVertexBrush, null, point, vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
            }
            else
            {
                context.DrawEllipse(VertexBrush, null, point, vertex.Size * _scene.Zoom, vertex.Size * _scene.Zoom);
            }
        }
        /// <summary>
        /// Отрисовать сетку
        /// </summary>
        /// <param name="context">Контекст отрисовки</param>
        /// <param name="gridSize">Размер сетки</param>
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
