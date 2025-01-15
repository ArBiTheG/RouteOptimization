using Avalonia.Controls;
using Avalonia.Input;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Styles;
using RouteOptimization.Controls;
using RouteOptimization.Utils;
using System;

namespace RouteOptimization.Views.Pages.DataEditors
{
    public partial class LocationsEditorView : UserControl
    {
        private LabelStyle _cursorStyle;
        private PointFeature _cursorPoint;

        public LocationsEditorView()
        {
            InitializeComponent();

            //mapControl.PointerPressed += MapControl_PointerPressed;
            mapControl.Tapped += MapControl_Tapped;

            nameTextBox.TextChanged += NameTextBox_TextChanged;
            locationXTextBox.TextChanged += LocationXTextBox_TextChanged; ;
            locationYTextBox.TextChanged += LocationYTextBox_TextChanged; ;

            var symbolStyle = MapsuiTools.CreateSymbolStyle();
            _cursorStyle = MapsuiTools.CreateLabelStyle();

            _cursorPoint = MapsuiTools.CreatePointFeature(symbolStyle, _cursorStyle);

            mapControl.Map = CreateMap();
        }

        private void NameTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                _cursorStyle.Text = textBox.Text;
                mapControl.RefreshGraphics();
            }
        }

        private void LocationXTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                _cursorPoint.Point.X = Convert.ToDouble(textBox.Text);
                mapControl.RefreshGraphics();
            }
        }

        private void LocationYTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                _cursorPoint.Point.Y = Convert.ToDouble(textBox.Text);
                mapControl.RefreshGraphics();
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
                    locationXTextBox.Text = worldPosition.X.ToString();
                    locationYTextBox.Text = worldPosition.Y.ToString();
                }
            }
        }

        private Map CreateMap()
        {
            return new MapBuilder()
                .SetOpenStreetMapLayer()
                .SetWritableLayer(MapsuiTools.CreateLayer("PointLayer", _cursorPoint))
                .SetTextWidget("Укажите на карте")
                .SetCoordinatesWidget()
                .SetScaleBarWidget()
                .SetBoundsFromLonLat(22.0, 34.0, 180, 80.0)
                .SetBoundsLayerFromLonLat(26.9, 40.3, 180, 74.6)
                .SetHome(4337667, 5793728, 50)
                .Build();
        }
    }
}
