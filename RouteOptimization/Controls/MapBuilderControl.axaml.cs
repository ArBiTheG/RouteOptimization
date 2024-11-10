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
using System.Collections.Specialized;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using SkiaSharp;
using System.Reflection.Metadata;
using Mapsui.UI.Avalonia;
using Mapsui.UI;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Widgets.Zoom;
using Mapsui.Widgets.ScaleBar;
using Mapsui.Rendering.Skia.SkiaWidgets;
using Mapsui.Widgets.MouseCoordinatesWidget;
using Mapsui.Projections;
using Mapsui.Limiting;
using Mapsui.Layers;
using Mapsui.Styles;
using Mapsui.Widgets;
using static System.Net.Mime.MediaTypeNames;

namespace RouteOptimization.Controls
{
    public partial class MapBuilderControl : UserControl
    {
        MapControl _mapControl;

        public MapBuilderControl()
        {
            Focusable = true;
            InitializeComponent();

            _mapControl = new MapControl();
            _mapControl.Map = InitializeMap();

            Content = _mapControl;
        }

        public Map InitializeMap()
        {
            var map = new Map();


            var textBox = new Mapsui.Widgets.TextBox()
            {
                VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top,
                HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center,
                Text = "Постройте карту",
                MarginY = 20,
                PaddingX = 20,
                PaddingY = 5
            };

            var mouseCoordinatesWidget = new MouseCoordinatesWidget(map)
            {
                VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Bottom
            };

            var scaleBarWidget = new ScaleBarWidget(map)
            {
                MarginX = 20,
                MarginY = 20
            };

            var zoomInOutWidget = new ZoomInOutWidget()
            {
                MarginX = 20,
                MarginY = 20
            };

            map.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
            map.Widgets.Add(textBox);
            map.Widgets.Add(mouseCoordinatesWidget);
            map.Widgets.Add(scaleBarWidget);
            map.Widgets.Add(zoomInOutWidget);

            var panBounds = GetPanBoundsOfRussia();
            map.Layers.Add(CreatePanBoundsLayer(panBounds));

            map.Navigator.Limiter = new ViewportLimiterKeepWithinExtent();
            map.Navigator.RotationLock = true;
            map.Navigator.OverridePanBounds = GetLimitOfRussia();
            map.Navigator.OverrideZoomBounds = new MMinMax(0.6, 10000);

            map.Home = n => n.ZoomToBox(panBounds);


            return map;
        }
        private static MemoryLayer CreatePanBoundsLayer(MRect panBounds)
        {
            // This layer is only for visualizing the pan bounds. It is not needed for the limiter.
            return new MemoryLayer("PanBounds")
            {
                Features = new[] { new RectFeature(panBounds) },
                Style = new VectorStyle() { Fill = null, Outline = new Pen(Color.Blue, 2) { PenStyle = PenStyle.ShortDash } }
            };
        }
        private static MRect GetPanBoundsOfRussia()
        {
            var (minX, minY) = SphericalMercator.FromLonLat(26.9, 40.3);
            var (maxX, maxY) = SphericalMercator.FromLonLat(180, 74.6);
            return new MRect(minX, minY, maxX, maxY);
        }
        private static MRect GetLimitOfRussia()
        {
            var (minX, minY) = SphericalMercator.FromLonLat(22.0, 34.0);
            var (maxX, maxY) = SphericalMercator.FromLonLat(180, 80.0);
            return new MRect(minX, minY, maxX, maxY);
        }
    }
}
