using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Limiting;
using Mapsui.Nts.Editing;
using Mapsui.Nts.Widgets;
using Mapsui.Projections;
using Mapsui.Styles;
using Mapsui.Styles.Thematics;
using Mapsui.UI;
using Mapsui.Widgets.MouseCoordinatesWidget;
using Mapsui.Widgets.ScaleBar;
using Mapsui.Widgets.Zoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls
{
    public class MapBuilder
    {
        Map _map;
        MapConstructorManager _mapManager;

        public MapBuilder()
        {
            _map = new Map();
        }

        public MapBuilder SetOpenStreetMapLayer()
        {
            _map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
            return this;
        }

        public MapBuilder SetBoundsLayer(double x1, double y1, double x2, double y2)
        {
            var panBounds = new MRect(x1, y1, x2, y2);

            var memoryLayer = new MemoryLayer("PanBounds")
            {
                Features = new[] { new RectFeature(panBounds) },
                Style = new VectorStyle() { Fill = null, Outline = new Pen(Color.Blue, 2) { PenStyle = PenStyle.ShortDash } }
            };

            _map.Layers.Add(memoryLayer);

            return this;
        }

        public MapBuilder SetBoundsLayerFromLonLat(double lon1, double lat1, double lon2, double lat2)
        {
            var (minX, minY) = SphericalMercator.FromLonLat(lon1, lat1);
            var (maxX, maxY) = SphericalMercator.FromLonLat(lon2, lat2);
            var panBounds = new MRect(minX, minY, maxX, maxY);

            var memoryLayer = new MemoryLayer("PanBounds")
            {
                Features = new[] { new RectFeature(panBounds) },
                Style = new VectorStyle() { Fill = null, Outline = new Pen(Color.Blue, 2) { PenStyle = PenStyle.ShortDash } }
            };

            _map.Layers.Add(memoryLayer);

            return this;
        }

        public MapBuilder SetBounds(double x1, double y1, double x2, double y2, double lat2)
        {
            var panBounds = new MRect(x1, y1, x2, y2);

            _map.Navigator.Limiter = new ViewportLimiterKeepWithinExtent();
            _map.Navigator.RotationLock = true;
            _map.Navigator.OverridePanBounds = panBounds;
            _map.Navigator.OverrideZoomBounds = new MMinMax(0.6, 8000);

            return this;
        }

        public MapBuilder SetBoundsFromLonLat(double lon1, double lat1, double lon2, double lat2)
        {
            var (minX, minY) = SphericalMercator.FromLonLat(lon1, lat1);
            var (maxX, maxY) = SphericalMercator.FromLonLat(lon2, lat2);
            var panBounds = new MRect(minX, minY, maxX, maxY);

            _map.Navigator.Limiter = new ViewportLimiterKeepWithinExtent();
            _map.Navigator.RotationLock = true;
            _map.Navigator.OverridePanBounds = panBounds;
            _map.Navigator.OverrideZoomBounds = new MMinMax(0.6, 8000);

            return this;
        }

        public MapBuilder SetHome(double x, double y, double resolution)
        {
            MPoint point = new MPoint(x, y);

            _map.Home = n => n.CenterOnAndZoomTo(point, resolution);
            return this;
        }

        public MapBuilder SetHomeFromLonLat(double lon, double lat, double resolution)
        {
            var (x, y) = SphericalMercator.FromLonLat(lon, lat);
            MPoint point = new MPoint(x, y);

            _map.Home = n => n.CenterOnAndZoomTo(point, resolution);
            return this;
        }

        public MapBuilder SetWritableLayer(WritableLayer layer)
        {
            _map.Layers.Add(layer);

            return this;
        }

        public MapBuilder SetTextWidget(string text)
        {
            var textBox = new Mapsui.Widgets.TextBox()
            {
                VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top,
                HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center,
                Text = text,
                MarginY = 10,
                PaddingX = 10,
                PaddingY = 5
            };

            _map.Widgets.Add(textBox);
            return this;
        }

        public MapBuilder SetCoordinatesWidget()
        {
            var mouseCoordinatesWidget = new MouseCoordinatesWidget(_map)
            {
                VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom,
            };
            _map.Widgets.Add(mouseCoordinatesWidget);
            return this;
        }

        public MapBuilder SetScaleBarWidget()
        {
            var scaleBarWidget = new ScaleBarWidget(_map)
            {
                MarginX = 10,
                MarginY = 10
            };
            _map.Widgets.Add(scaleBarWidget);
            return this;
        }

        public MapBuilder SetZoomWidget()
        {
            var zoomInOutWidget = new ZoomInOutWidget()
            {
                MarginX = 10,
                MarginY = 10
            };
            _map.Widgets.Add(zoomInOutWidget);
            return this;
        }

        public Map Build()
        {
            return _map;
        }

    }
}
