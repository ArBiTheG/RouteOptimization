using Avalonia.Media;
using Avalonia;
using System;
using SkiaSharp;
using RouteOptimization.Controls.MapBuilder;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.OpenGL.Egl;
using Avalonia.Controls;
using RouteOptimization.Utils;

namespace RouteOptimization.Controls
{
    public partial class MapBuilderControl
    {
        class RenderingLogic : ICustomDrawOperation
        {
            public Action<SKCanvas>? RenderCall;
            public Rect Bounds { get; set; }

            public void Dispose() { }

            public bool Equals(ICustomDrawOperation? other) => other == this;

            public bool HitTest(Point p) { return true; }

            public void Render(ImmediateDrawingContext context)
            {
                var skia = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
                if (skia != null)
                {
                    using (var lease = skia.Lease())
                    {
                        SKCanvas canvas = lease.SkCanvas;
                        if (canvas != null)
                        {
                            RenderCall?.Invoke(canvas);
                        }
                    }
                }
            }
        }

        private IPen _penEdge;
        private IPen _penEdgeFocused;
        private IPen _penGridScene;

        private RenderingLogic renderingLogic;

        public event Action<SKCanvas> RenderSkia;

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

        private void InitializeVisual()
        {
            _penEdge = new Pen(EdgeBrush);
            _penEdgeFocused = new Pen(FocusedEdgeBrush);
            _penGridScene = new Pen(GridBrush);

            RenderSkia += HandleRenderSkia;

            renderingLogic = new RenderingLogic();
            renderingLogic.RenderCall += (canvas) => RenderSkia?.Invoke(canvas);
        }

        public override void Render(DrawingContext context)
        {
            renderingLogic.Bounds = new Rect(0, 0, Bounds.Width, Bounds.Height);
            context.Custom(renderingLogic);
        }

        /// <summary>
        /// Отрисовка окна средствами SkiaSharp
        /// </summary>
        /// <param name="canvas">Холст для отрисовки</param>
        private void HandleRenderSkia(SKCanvas canvas)
        {
            float width = canvas.DeviceClipBounds.Width;
            float height = canvas.DeviceClipBounds.Height;

            var backgroudPaint = new SKPaint { Color = new SKColor(64,64,64) };

            canvas.Clear(backgroudPaint.Color);

            if (_scene != null)
            {
                DrawGrid(canvas, _scene, 50);

                if (_edges != null)
                {
                    foreach (IEdge edge in _edges)
                    {
                        DrawEdge(canvas, _scene, edge);
                    }
                }

                if (_vertices != null)
                {
                    foreach (IVertex vertex in _vertices)
                    {
                        DrawVertex(canvas, _scene, vertex);
                    }
                }

#if DEBUG
                DrawDebugInfo(canvas, _scene);
#endif
            }
        }

        /// <summary>
        /// Отрисовать ребро между первой и второй точкой
        /// </summary>
        /// <param name="canvas">Холст для отрисовки</param>
        /// <param name="edge">Объект ребра</param>
        private void DrawEdge(SKCanvas canvas, Scene scene, IEdge edge)
        {
            if (scene == null) return;
            if (edge == null) return;

            float width = canvas.LocalClipBounds.Width;
            float height = canvas.LocalClipBounds.Height;

            var startEdgeDisplay = GetPositionOnDisplay(new ScenePoint(edge.StartX, edge.StartY), scene, width, height);
            var finishEdgeDisplay = GetPositionOnDisplay(new ScenePoint(edge.FinishX, edge.FinishY), scene, width, height);

            if (_focusedEdge == edge)
            {
                canvas.DrawLine(startEdgeDisplay.ToSKPoint(), finishEdgeDisplay.ToSKPoint(), new SKPaint() { Color = new SKColor(0, 0, 128) });
            }
            else
            {
                canvas.DrawLine(startEdgeDisplay.ToSKPoint(), finishEdgeDisplay.ToSKPoint(), new SKPaint() { Color = new SKColor(64, 64, 128) });
            }
        }
        
        /// <summary>
        /// Отрисовать точку
        /// </summary>
        /// <param name="canvas">Холст для отрисовки</param>
        /// <param name="vertex">Объект точки</param>
        private void DrawVertex(SKCanvas canvas, Scene scene, IVertex vertex)
        {
            if (scene == null) return;
            if (vertex == null) return;

            float width = canvas.LocalClipBounds.Width;
            float height = canvas.LocalClipBounds.Height;

            var vertexDisplay = GetPositionOnDisplay(vertex, scene, width, height);

            if (_selectedVertex == vertex)
            {
                var paint = new SKPaint() { Color = new SKColor(0, 0, 128) };
                canvas.DrawCircle(vertexDisplay.ToSKPoint(), vertex.Size * scene.Zoom, new SKPaint() { Color = new SKColor(0, 0, 128) });
#if DEBUG
                var test1 = GetPositionOnDisplay(_selectedVertex, scene, width, height);
                canvas.DrawLine((float)_cursorDisplayCurrentPosition.X, (float)_cursorDisplayCurrentPosition.Y, (float)test1.X, (float)test1.Y, paint);
#endif
            }
            else if (_focusedVertex == vertex)
            {
                canvas.DrawCircle(vertexDisplay.ToSKPoint(), vertex.Size * scene.Zoom, new SKPaint() { Color = new SKColor(64, 64, 128) });
            }
            else
            {
                canvas.DrawCircle(vertexDisplay.ToSKPoint(), vertex.Size * scene.Zoom, new SKPaint() { Color = new SKColor(0, 0, 64) });
            }
        }
        
        /// <summary>
        /// Отрисовать сетку
        /// </summary>
        /// <param name="canvas">Холст для отрисовки</param>
        /// <param name="gridSize">Размер сетки</param>
        private void DrawGrid(SKCanvas canvas, Scene scene, float gridSize)
        {
            if (scene == null) return;

            var gridPaint = new SKPaint { Color = new SKColor(128, 128, 128) };

            float width = canvas.LocalClipBounds.Width;
            float height = canvas.LocalClipBounds.Height;

            float step = gridSize * _scene.Zoom;
            float x_start = (step + width / 2 - _scene.X * _scene.Zoom) % step;
            float x = x_start;
            float y = (step + height / 2 - _scene.Y * _scene.Zoom) % step;

            while (y < height)
            {
                canvas.DrawLine(x, 0, x, height, gridPaint);
                x += step;

                if (x >= width)
                {
                    x = x_start;
                    y += step;
                }

                canvas.DrawLine(0, y, width, y, gridPaint);
            }
        }

#if DEBUG
        private void DrawDebugInfo(SKCanvas canvas, Scene scene)
        {
            if (scene == null) return;

            float width = canvas.DeviceClipBounds.Width;
            float height = canvas.DeviceClipBounds.Height;

            var textPaint = new SKPaint { Color = new SKColor(255, 255, 255) };
            int line = 1;
            canvas.DrawText(SKTextBlob.Create($"Размер интерфейса", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            canvas.DrawText(SKTextBlob.Create($"Ширина={width} | Высота={height}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);

            canvas.DrawText(SKTextBlob.Create($"Позиция сцены", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            canvas.DrawText(SKTextBlob.Create($"X={scene.X} | Y={scene.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);
            line++;

            canvas.DrawText(SKTextBlob.Create("Позиция курсора в интерфейсе: ", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            canvas.DrawText(SKTextBlob.Create($"X={_cursorDisplayCurrentPosition.X} | Y={_cursorDisplayCurrentPosition.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);

            Point pointRelativeToCenter = GetPointRelativeToCenter(_cursorDisplayCurrentPosition, width, height);
            canvas.DrawText(SKTextBlob.Create("Позиция курсора относительно центра в интерфейсе: ", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            canvas.DrawText(SKTextBlob.Create($"X={pointRelativeToCenter.X} | Y={pointRelativeToCenter.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);

            canvas.DrawLine((float)_cursorDisplayCurrentPosition.X, (float)_cursorDisplayCurrentPosition.Y, (float)_cursorDisplayCurrentPosition.X - (float)pointRelativeToCenter.X, (float)_cursorDisplayCurrentPosition.Y - (float)pointRelativeToCenter.Y, textPaint);


            ScenePoint pointRelativeToCenterScene = GetPositionOnScene(_cursorDisplayCurrentPosition, scene, width, height);
            canvas.DrawText(SKTextBlob.Create("Позиция курсора относительно центра на сцене: ", new SKFont(SKTypeface.Default, 12)), 00, 15 * line++, textPaint);
            canvas.DrawText(SKTextBlob.Create($"X={pointRelativeToCenterScene.X} | Y={pointRelativeToCenterScene.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);

            var test2 = GetSceneCenterPosition(scene, width, height);
            canvas.DrawLine((float)_cursorDisplayCurrentPosition.X, (float)_cursorDisplayCurrentPosition.Y, (float)test2.X, (float)test2.Y, textPaint);
            line++;

            canvas.DrawText(SKTextBlob.Create("Место последнего нажатия курсора в интерфейсе: ", new SKFont(SKTypeface.Default, 12)), 00, 15 * line++, textPaint);
            canvas.DrawText(SKTextBlob.Create($"X={_cursorDisplayPreviousPressPosition.X} | Y={_cursorDisplayPreviousPressPosition.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);

            Point pointOffset = GetOffsetPosition(_cursorDisplayCurrentPosition, _cursorDisplayPreviousPressPosition);
            canvas.DrawText(SKTextBlob.Create("Позиция смещения курсора с места последнего нажатия в интерфейсе: ", new SKFont(SKTypeface.Default, 12)), 00, 15 * line++, textPaint);
            canvas.DrawText(SKTextBlob.Create($"X={pointOffset.X} | Y={pointOffset.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);

            canvas.DrawLine((float)_cursorDisplayCurrentPosition.X, (float)_cursorDisplayCurrentPosition.Y, (float)_cursorDisplayPreviousPressPosition.X, (float)_cursorDisplayPreviousPressPosition.Y, textPaint);
            //
            //Point pressPointRelativeToCenter = GetPointOffsetRelativeToCenter(_cursorScenePositionX, _cursorScenePositionY, _pointerLastPressX, _pointerLastPressY, width, height);
            //canvas.DrawText(SKTextBlob.Create("Позиция курсора относительно центра в интерфейсе: ", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            //canvas.DrawText(SKTextBlob.Create($"X={pressPointRelativeToCenter.X} | Y={pressPointRelativeToCenter.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);
            //
            //Point pressPointRelativeToCenterScene = GetPointOffsetRelativeToScene(_cursorScenePositionX, _cursorScenePositionY, _pointerLastPressX, _pointerLastPressY, width, height, scene);
            //canvas.DrawText(SKTextBlob.Create("Позиция курсора относительно центра на сцене: ", new SKFont(SKTypeface.Default, 12)), 00, 15 * line++, textPaint);
            //canvas.DrawText(SKTextBlob.Create($"X={pressPointRelativeToCenterScene.X} | Y={pressPointRelativeToCenterScene.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);


            line++;

            if (_selectedVertex != null)
            {
                canvas.DrawText(SKTextBlob.Create("Выбранная точка", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
                canvas.DrawText(SKTextBlob.Create($"Id={_selectedVertex.Id}, Hash={_selectedVertex.GetHashCode()}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);
                canvas.DrawText(SKTextBlob.Create("Позиция точки на сцене", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
                canvas.DrawText(SKTextBlob.Create($"X={_selectedVertex.X}, Y={_selectedVertex.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);

                var test1 = GetPositionOnDisplay(_selectedVertex, scene, width, height);
                canvas.DrawText(SKTextBlob.Create("Позиция точки в интерфейсе", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
                canvas.DrawText(SKTextBlob.Create($"X={test1.X}, Y={test1.Y}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);

                var test5 = GetPointRelativeToCenter(_cursorDisplayCurrentPosition, width, height);
                var dist = RouteMath.Distance(_cursorDisplayCurrentPosition.X, _cursorDisplayCurrentPosition.Y, test1.X, test1.Y);
                canvas.DrawText(SKTextBlob.Create($"Расстояние до точки: {dist}", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            }

            line++;

            if (_focusedVertex != null)
            {
                canvas.DrawText(SKTextBlob.Create("Точка на фокусе", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
                canvas.DrawText(SKTextBlob.Create($"Id={_focusedVertex.Id}, X={_focusedVertex.X}, X={_focusedVertex.Y}, Hash={_focusedVertex.GetHashCode()}", new SKFont(SKTypeface.Default, 12)), 30, 15 * line++, textPaint);
            }

            line++;

            canvas.DrawText(SKTextBlob.Create("Нажатые кнопки", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            if (_keyUpPressModifier == 1)
            {
                canvas.DrawText(SKTextBlob.Create("Вверх", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            }
            if (_keyDownPressModifier == 1)
            {
                canvas.DrawText(SKTextBlob.Create("Вниз", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            }
            if (_keyLeftPressModifier == 1)
            {
                canvas.DrawText(SKTextBlob.Create("Налево", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            }
            if (_keyRightPressModifier == 1)
            {
                canvas.DrawText(SKTextBlob.Create("Направо", new SKFont(SKTypeface.Default, 12)), 0, 15 * line++, textPaint);
            }

        }
#endif
    }
}
