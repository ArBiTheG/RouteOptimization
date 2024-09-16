using RouteOptimization.WpfApp.Controls.Entities;
using RouteOptimization.WpfApp.Utilties;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RouteOptimization.WpfApp.Controls
{
    /// <summary>
    /// Логика взаимодействия для MapBuilderControl.xaml
    /// </summary>
    public partial class MapBuilderControl : UserControl
    {
        private Brush _brushBackground;
        private Brush _brushVertex;
        private Brush _brushVertexSelected;
        private Brush _brushVertexFocused;

        private Pen _penEdge;
        private Pen _penEdgeSelected;
        private Pen _penEdgeFocused;

        private Pen _penGridScene;

        private double _pointerLastPressX;
        private double _pointerLastPressY;
        private Scene _scene = new Scene(0, 0);
        private IEnumerable<Vertex> _vertices = new List<Vertex>();
        private IEnumerable<Edge> _edges = new List<Edge>();

        public MapBuilderControl()
        {
            InitializeComponent();

            _brushBackground = new SolidColorBrush(Color.FromArgb(127, 25, 25, 50));

            _penGridScene = new Pen(new SolidColorBrush(Color.FromArgb(255, 25, 25, 50)),1);
        }

        protected override void OnRender(DrawingContext context)
        {
            base.OnRender(context);

            Rect bgRect = new Rect(0, 0, ActualWidth, ActualHeight);

            if (_brushBackground != null)
            {
                context.DrawRectangle(_brushBackground, null, bgRect);
            };

            if (_scene.Zoom < 0.01)
            {
                DrawGrid(context, bgRect, 5000);
            }
            else if (_scene.Zoom <= 0.10)
            {
                DrawGrid(context, bgRect, 500);
            }
            else if (_scene.Zoom <= 0.25)
            {
                DrawGrid(context, bgRect, 200);
            }
            else if (_scene.Zoom <= 0.50)
            {
                DrawGrid(context, bgRect, 100);
            }
            else if (_scene.Zoom <= 5.0)
            {
                DrawGrid(context, bgRect, 50);
            }
            else if (_scene.Zoom > 5.00)
            {
                DrawGrid(context, bgRect, 5);
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
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var pointerPosition = e.GetPosition(this);
            _pointerLastPressX = pointerPosition.X;
            _pointerLastPressY = pointerPosition.Y;

            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                _scene.PerformPress();
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                bool oneSelected = false;

                foreach (Vertex vertex in _vertices.Reverse())
                {
                    double cursorX = (pointerPosition.X - ActualWidth / 2) / _scene.Zoom + _scene.X;
                    double cursorY = (pointerPosition.Y - ActualHeight / 2) / _scene.Zoom + _scene.Y;

                    if (RouteMath.CursorInPoint(vertex.X, vertex.Y, cursorX, cursorY, vertex.Size) && oneSelected == false)
                    {
                        oneSelected = true;
                        vertex.PerformPress();
                    }
                    else
                    {
                        vertex.PerformRelease();
                    }
                }
                foreach (Edge edge in _edges.Reverse())
                {
                    double cursorX = (pointerPosition.X - ActualWidth / 2) / _scene.Zoom + _scene.X;
                    double cursorY = (pointerPosition.Y - ActualHeight / 2) / _scene.Zoom + _scene.Y;

                    if (RouteMath.CursorInLine(edge.VertexFrom.X, edge.VertexFrom.Y, edge.VertexTo.X, edge.VertexTo.Y, cursorX, cursorY, 4) && oneSelected == false)
                    {
                        oneSelected = true;
                        edge.PerformPress();
                    }
                    else
                    {
                        edge.PerformRelease();
                    }
                }
            }
            InvalidateVisual();
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            var pointerPosition = e.GetPosition(this);
            if (e.MiddleButton == MouseButtonState.Released)
            {
                _scene.PerformRelease();
            }

            InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var pointerPosition = e.GetPosition(this);
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                _scene.PerformMove(new Point(
                                (_pointerLastPressX - pointerPosition.X) / _scene.Zoom,
                                (_pointerLastPressY - pointerPosition.Y) / _scene.Zoom
                                ));
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                foreach (Vertex vertex in _vertices)
                {
                    if (vertex.Selected)
                    {
                        vertex.PerformMove(new Point(
                            (pointerPosition.X - _pointerLastPressX) / _scene.Zoom,
                            (pointerPosition.Y - _pointerLastPressY) / _scene.Zoom
                            ));
                    }
                }
            }


            bool oneFocused = false;
            foreach (Vertex vertex in _vertices.Reverse())
            {
                double cursorX = (pointerPosition.X - ActualWidth / 2) / _scene.Zoom + _scene.X;
                double cursorY = (pointerPosition.Y - ActualHeight / 2) / _scene.Zoom + _scene.Y;

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
                double cursorX = (pointerPosition.X - ActualWidth / 2) / _scene.Zoom + _scene.X;
                double cursorY = (pointerPosition.Y - ActualHeight / 2) / _scene.Zoom + _scene.Y;

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

            InvalidateVisual();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            _scene.PerformWheelChanged(e.Delta);
            InvalidateVisual();
        }


        private Point GetDrawPosition(double x, double y)
        {
            return new Point(
                x * _scene.Zoom - _scene.X * _scene.Zoom + (ActualWidth / 2),
                y * _scene.Zoom - _scene.Y * _scene.Zoom + (ActualHeight / 2));
        }
        private void DrawGrid(DrawingContext context, Rect rect, double gridSize)
        {
            double step = gridSize * _scene.Zoom;
            double x_start = (step + rect.Width / 2 - _scene.X * _scene.Zoom) % step;
            double x = x_start;
            double y = (step + rect.Height / 2 - _scene.Y * _scene.Zoom) % step;

            while (y < rect.Height)
            {
                context.DrawLine(_penGridScene, new Point(x, 0), new Point(x, rect.Height));
                x += step;

                if (x >= rect.Width)
                {
                    x = x_start;
                    y += step;
                }

                context.DrawLine(_penGridScene, new Point(0, y), new Point(rect.Width, y));
            }
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

    }
}
