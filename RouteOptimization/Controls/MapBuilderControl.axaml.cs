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
        private sbyte _keyUpPressModifier = 0;
        private sbyte _keyDownPressModifier = 0;
        private sbyte _keyLeftPressModifier = 0;
        private sbyte _keyRightPressModifier = 0;

        private Point _cursorDisplayCurrentPosition = new Point(0, 0);
        private Point _cursorDisplayPreviousPressPosition = new Point(0,0);

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


            Focusable = true;
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
            var displaySize = Bounds.Size;
            var cursorPosition = e.GetPosition(this);
            _cursorDisplayCurrentPosition = cursorPosition;
            _cursorDisplayPreviousPressPosition = cursorPosition;

            if (e.Pointer.Type == PointerType.Mouse)
            {
                var cursorProperties = e.GetCurrentPoint(this).Properties;
                if (cursorProperties.IsLeftButtonPressed)
                {
                    SelectedVertex = null;
                    var cursorScenePosition = GetPositionOnScene(cursorPosition, _scene, displaySize.Width, displaySize.Height);

                    foreach (Location vertex in _vertices.Reverse())
                    {
                        if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorScenePosition.X, cursorScenePosition.Y, vertex.Size))
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
            _cursorDisplayCurrentPosition = cursorPosition;
            _cursorDisplayPreviousPressPosition = new Point(0,0);

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
            var displaySize = Bounds.Size;

            var cursorPosition = e.GetPosition(this);
            _cursorDisplayCurrentPosition = cursorPosition;

            if (e.Pointer.Type == PointerType.Mouse)
            {
                var cursorProperties = e.GetCurrentPoint(this).Properties;
                if (cursorProperties.IsLeftButtonPressed)
                {
                    if (_selectedVertex != null)
                    {
                        VertexUI.PerformMove(_selectedVertex, GetOffsetPositionOnScene(cursorPosition, _cursorDisplayPreviousPressPosition, _scene));

                        goto draw;
                    }
                    else
                    {
                        Scene.PerformMove(_scene, GetOffsetPositionOnScene(_cursorDisplayPreviousPressPosition, cursorPosition, _scene));

                        goto draw;
                    }
                }

                // Фокусирование
                _focusedEdge = null;
                _focusedVertex = null;
                var cursorScenePosition = GetPositionOnScene(cursorPosition, _scene, displaySize.Width, displaySize.Height);

                foreach (Location vertex in _vertices.Reverse())
                {
                    if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorScenePosition.X, cursorScenePosition.Y, vertex.Size))
                    {
                        _focusedVertex = vertex;

                        goto draw;
                    }
                }
                foreach (Route edge in _edges.Reverse())
                {
                    if (RouteMath.CursorInLine(edge.StartX, edge.StartY, edge.FinishX, edge.FinishY, cursorScenePosition.X, cursorScenePosition.Y, 4))
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
            var displaySize = Bounds.Size;

            if (e.Pointer.Type == PointerType.Mouse)
            {
                //Scene.PerformMove(_scene, GetPositionOnScene(_cursorDisplayCurrentPosition, _scene, displaySize.Width, displaySize.Height));

                Scene.PerformWheelChanged(_scene, e.Delta);
            }

            InvalidateVisual();
        }

        protected override void OnGotFocus(GotFocusEventArgs e)
        {
            InvalidateVisual();
        }
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            _cursorDisplayPreviousPressPosition = new Point(0, 0);
            _keyUpPressModifier = 0;
            _keyDownPressModifier = 0;
            _keyLeftPressModifier = 0;
            _keyRightPressModifier = 0;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyDeviceType == KeyDeviceType.Keyboard)
            {
                double speed = 1;

                if (e.KeyModifiers.HasFlag(KeyModifiers.Shift))
                    speed = 5;

                switch (e.Key)
                {
                    case Key.Up:
                        _keyUpPressModifier = 1;
                        break;
                    case Key.Down:
                        _keyDownPressModifier = 1;
                        break;
                    case Key.Left:
                        _keyLeftPressModifier = 1;
                        break;
                    case Key.Right:
                        _keyRightPressModifier = 1;
                        break;
                }

                ScenePoint arrowMove = new ScenePoint(_keyRightPressModifier - _keyLeftPressModifier, _keyDownPressModifier - _keyUpPressModifier);

                if (_selectedVertex != null)
                {

                    VertexUI.PerformPress(_selectedVertex);
                    VertexUI.PerformMove(_selectedVertex, GetScalePosition(arrowMove, _scene.Zoom / speed));

                    goto draw;
                }
                else
                {
                    Scene.PerformPress(_scene);
                    Scene.PerformMove(_scene, GetScalePosition(arrowMove, _scene.Zoom / speed));

                    goto draw;
                }
            }
            draw: InvalidateVisual();
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    _keyUpPressModifier = 0;
                    break;
                case Key.Down:
                    _keyDownPressModifier = 0;
                    break;
                case Key.Left:
                    _keyLeftPressModifier = 0;
                    break;
                case Key.Right:
                    _keyRightPressModifier = 0;
                    break;
            }
            InvalidateVisual();
        }

        #endregion

        public static Point GetCenterDisplay(double displayWidth, double displayHeight) =>
            new(displayWidth / 2, displayHeight / 2);

        /// <summary>
        /// Получить положение точки относительно центра дисплея
        /// </summary>
        /// <param name="displayPoint">Точка на дисплее</param>
        /// <param name="displayWidth">Отрисовываемая ширина</param>
        /// <param name="displayHeight">Отрисовываемая высота</param>
        /// <returns></returns>
        public static Point GetPointRelativeToCenter(Point displayPoint, double displayWidth, double displayHeight) => 
            new(displayPoint.X - displayWidth / 2, displayPoint.Y - displayHeight / 2);

        /// <summary>
        /// Получить позицию сцены
        /// </summary>
        /// <param name="displayPoint">Точка на дисплее</param>
        /// <param name="scene">Объект сцены</param>
        /// <param name="displayWidth">Отрисовываемая ширина</param>
        /// <param name="displayHeight">Отрисовываемая высота</param>
        /// <returns>Возращает позицию точки на сцене</returns>
        public static ScenePoint GetPositionOnScene(Point displayPoint, Scene scene, double displayWidth, double displayHeight) => 
            new((displayPoint.X - displayWidth / 2) / scene.Zoom + scene.X, (displayPoint.Y - displayHeight / 2) / scene.Zoom + scene.Y);

        /// <summary>
        /// Получить положение центра сцены на дисплее
        /// </summary>
        /// <param name="scene">Объект сцены</param>
        /// <param name="displayWidth">Отрисовываемая ширина</param>
        /// <param name="displayHeight">Отрисовываемая высота</param>
        /// <returns>Возращает реальную позицию на холсте</returns>
        public static Point GetSceneCenterPosition(Scene scene, double displayWidth, double displayHeight) =>
            new(displayWidth / 2 - scene.X * scene.Zoom, displayHeight / 2 - scene.Y * scene.Zoom);

        /// <summary>
        /// Получить положение точки относительно центра сцены на дисплее
        /// </summary>
        /// <param name="scenePoint">Точка на сцене</param>
        /// <param name="scene">Объект сцены</param>
        /// <param name="renderWidth">Отрисовываемая ширина</param>
        /// <param name="renderHeight">Отрисовываемая высота</param>
        /// <returns>Возращает реальную позицию точки на холсте</returns>
        public static Point GetPositionOnDisplay(IScenePoint scenePoint, Scene scene, double renderWidth, double renderHeight) =>
            new(scenePoint.X * scene.Zoom - scene.X * scene.Zoom + renderWidth / 2, scenePoint.Y * scene.Zoom - scene.Y * scene.Zoom + renderHeight / 2);

        /// <summary>
        /// Получить разницу между двумя точками
        /// </summary>
        /// <param name="displayPoint1">Первая точка</param>
        /// <param name="displayPoint2">Вторая точка</param>
        /// <returns>Возращает полицию описывающую разницу</returns>
        public static Point GetOffsetPosition(Point displayPoint1, Point displayPoint2) => 
            new(displayPoint1.X - displayPoint2.X, displayPoint1.Y - displayPoint2.Y);

        /// <summary>
        /// Получить разницу между двумя точками на сцене
        /// </summary>
        /// <param name="displayPoint1">Первая точка</param>
        /// <param name="displayPoint2">Вторая точка</param>
        /// <param name="scene">Объект сцены</param>
        /// <returns>Возращает полицию описывающую разницу</returns>
        public static ScenePoint GetOffsetPositionOnScene(Point displayPoint1, Point displayPoint2, Scene scene) =>
            new((displayPoint1.X - displayPoint2.X) / scene.Zoom, (displayPoint1.Y - displayPoint2.Y) / scene.Zoom);

        /// <summary>
        /// Получить разницу между двумя точками
        /// </summary>
        /// <param name="scenePoint1">Первая точка</param>
        /// <param name="scenePoint2">Вторая точка</param>
        /// <returns>Возращает полицию описывающую разницу</returns>
        public static ScenePoint GetOffsetPosition(ScenePoint scenePoint1, ScenePoint scenePoint2) =>
            new(scenePoint1.X - scenePoint2.X, scenePoint1.Y - scenePoint2.Y);

        public static ScenePoint GetScalePosition(ScenePoint scenePoint, double zoom) =>
            new(scenePoint.X / zoom, scenePoint.Y / zoom);

    }
}
