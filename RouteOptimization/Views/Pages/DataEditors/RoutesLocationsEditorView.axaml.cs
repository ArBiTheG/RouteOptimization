using Avalonia.Controls;
using Mapsui.Layers;
using Mapsui;
using RouteOptimization.Controls;
using Mapsui.Nts;
using Avalonia.Input;
using System.Collections.Generic;
using System.Linq;
using NetTopologySuite.IO;
using System;
using RouteOptimization.Models.Entities;
using Mapsui.Styles;
using System.Security.Cryptography.X509Certificates;
using NetTopologySuite.Geometries;
using RouteOptimization.Utils;

namespace RouteOptimization.Views.Pages.DataEditors
{
    public partial class RoutesLocationsEditorView : UserControl
    {
        private GeometryFeature _geometryFeature;

        private PointFeature _pointStartFeature;
        private LabelStyle _pointStartStyle;

        private PointFeature _pointFinishFeature;
        private LabelStyle _pointFinishStyle;

        public RoutesLocationsEditorView()
        {
            InitializeComponent();

            mapControl.Tapped += MapControl_Tapped;
            locationStartComboBox.SelectionChanged += LocationComboBox_SelectionChanged;
            locationFinishComboBox.SelectionChanged += LocationComboBox_SelectionChanged;

            var symbolStyle = MapsuiTools.CreateSymbolStyle();
            var vectorStyle = MapsuiTools.CreateVectorStyle();
            _pointStartStyle = MapsuiTools.CreateLabelStyle();
            _pointFinishStyle = MapsuiTools.CreateLabelStyle();

            _geometryFeature = MapsuiTools.CreateGeometryFeature(vectorStyle);
            _pointStartFeature = MapsuiTools.CreatePointFeature(0,0, symbolStyle, _pointStartStyle);
            _pointFinishFeature = MapsuiTools.CreatePointFeature(0,0, symbolStyle, _pointFinishStyle);

            mapControl.Map = CreateMap();
        }

        private void LocationComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var startLocation = locationStartComboBox.SelectedItem as ILocation;
            var finishLocation = locationFinishComboBox.SelectedItem as ILocation;
            if (startLocation != null)
            {
                _pointStartFeature.Point.X = startLocation.X;
                _pointStartFeature.Point.Y = startLocation.Y;
                _pointStartStyle.Text = startLocation.Name;
            }
            if (finishLocation != null)
            {
                _pointFinishFeature.Point.X = finishLocation.X;
                _pointFinishFeature.Point.Y = finishLocation.Y;
                _pointFinishStyle.Text = finishLocation.Name;
            }
            if (startLocation != null && finishLocation != null)
            {
                _geometryFeature.Geometry = MapsuiTools.GetGeometry(startLocation, finishLocation);
            }
        }

        private void MapControl_Tapped(object? sender, TappedEventArgs e)
        {
            var screenPosition = e.GetPosition(mapControl);
            if (e.Pointer.Type == PointerType.Mouse)
            {
                var worldPosition = mapControl.GetMapInfo(new MPoint(screenPosition.X, screenPosition.Y))?.WorldPosition;
                if (worldPosition != null)
                {
                }
            }
        }

        private Map CreateMap()
        {
            return new MapBuilder()
                .SetOpenStreetMapLayer()
                .SetWritableLayer(MapsuiTools.CreateLayer("LineLayer", _geometryFeature))
                .SetWritableLayer(MapsuiTools.CreateLayer("PointLayer", _pointStartFeature, _pointFinishFeature))
                .SetCoordinatesWidget()
                .SetScaleBarWidget()
                .SetBoundsFromLonLat(22.0, 34.0, 180, 80.0)
                .SetBoundsLayerFromLonLat(26.9, 40.3, 180, 74.6)
                .SetHome(4337667, 5793728, 50)
                .Build();
        }

    }
}
