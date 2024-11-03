using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DynamicData;
using RouteOptimization.Controls.MapBuilder;
using RouteOptimization.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using RouteOptimization.Models;
using Location = RouteOptimization.Models.Location;

namespace RouteOptimization.Controls
{
    public partial class MapBuilderControl : Control
    {
        private float _cursorScenePositionX;
        private float _cursorScenePositionY;

        private float _pointerLastPressX;
        private float _pointerLastPressY;

        private Scene _scene = new Scene(0, 0);
        private Location? _selectedVertex;
        private Location? _focusedVertex;
        private Route? _focusedEdge;
        private IEnumerable<Location> _vertices;
        private IEnumerable<Route> _edges;

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
            _vertices = new List<Location>();
            _edges = new List<Route>();

            InitializeVisual();
            InitializeComponent();
        }

        #region Override control methods

        protected override void OnLoaded(RoutedEventArgs e)
        {
            InvalidateVisual();
        }
        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            var cursorPosition = e.GetPosition(this);
            _pointerLastPressX = (float)cursorPosition.X;
            _pointerLastPressY = (float)cursorPosition.Y;

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
            _pointerLastPressX = 0;
            _pointerLastPressY = 0;

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
            _cursorScenePositionX = (float)cursorPosition.X;
            _cursorScenePositionY = (float)cursorPosition.Y;

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

                // �������������
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


        private Point GetRealPointRelativeToCenter(double x, double y, double renderWidth, double renderHeight) => 
            new(x - renderWidth / 2, y - renderHeight / 2);

        /// <summary>
        /// ������������� �������� ������� � ������� �����
        /// </summary>
        /// <param name="x">�������� ������� ����� �� X</param>
        /// <param name="y">�������� ������� ����� �� Y</param>
        /// <param name="scene">������ �����</param>
        /// <param name="renderWidth">�������������� ������</param>
        /// <param name="renderHeight">�������������� ������</param>
        /// <returns>��������� ������� ����� �� �����</returns>
        private Point ConvertToScenePosition(double x, double y, Scene scene, double renderWidth, double renderHeight) => 
            new((x - renderWidth / 2) / scene.Zoom + scene.X, (y - renderHeight / 2) / scene.Zoom + scene.Y);

        /// <summary>
        /// ������������� ����� ����� � �������� �������
        /// </summary>
        /// <param name="scene">������ �����</param>
        /// <param name="renderWidth">�������������� ������</param>
        /// <param name="renderHeight">�������������� ������</param>
        /// <returns>��������� �������� ������� �� ������</returns>
        private Point ConvertCenterSceneToRealPosition(Scene scene, double renderWidth, double renderHeight) =>
            new(renderWidth / 2 - scene.X * scene.Zoom, renderHeight / 2 - scene.Y * scene.Zoom);

        /// <summary>
        /// ������������� ����� ����� ����������� ������������ ����� � �������� �������
        /// </summary>
        /// <param name="x">������� X ����� �� �����</param>
        /// <param name="y">������� Y ����� �� �����</param>
        /// <param name="scene">������ �����</param>
        /// <param name="renderWidth">�������������� ������</param>
        /// <param name="renderHeight">�������������� ������</param>
        /// <returns>��������� �������� ������� ����� �� ������</returns>
        private Point ConvertPointSceneRelativeCenterToRealPosition(double x, double y, Scene scene, double renderWidth, double renderHeight) =>
            new(x * scene.Zoom - scene.X * scene.Zoom + renderWidth / 2,  y * scene.Zoom - scene.Y * scene.Zoom + renderHeight / 2);

        /// <summary>
        /// �������� ������� ����� ����� �������
        /// </summary>
        /// <param name="x1">������� X ������ �����</param>
        /// <param name="y1">������� Y ������ �����</param>
        /// <param name="x2">������� X ������ �����</param>
        /// <param name="y2">������� Y ������ �����</param>
        /// <returns>��������� ������� ����������� �������</returns>
        private Point GetPointOffset(double x1, double y1, double x2, double y2) => 
            new(x1 - x2, y1 - y2);

        private Point GetPointOffsetRelativeToCenter(double x1, double y1, double x2, double y2, double renderWidth, double renderHeight) =>
            new(x1 - x2 - renderWidth / 2, y1 - y2 - renderHeight / 2);

        private Point GetPointOffsetRelativeToScene(double x1, double y1, double x2, double y2, double renderWidth, double renderHeight, Scene scene) =>
            new((x1 - x2) / scene.Zoom + scene.X, (y1 - y2) / scene.Zoom + scene.Y);

    }
}
