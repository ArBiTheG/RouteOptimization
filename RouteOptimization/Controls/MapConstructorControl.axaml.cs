using Avalonia.Controls;
using Mapsui.Layers;
using Mapsui.Nts.Editing;
using Mapsui.Styles.Thematics;
using Mapsui.Styles;
using Mapsui.UI;
using Mapsui.UI.Avalonia;
using Mapsui;

namespace RouteOptimization.Controls
{
    public partial class MapConstructorControl : UserControl
    {
        MapControl _mapControl;

        MapConstructorManager _mapManager;

        public MapConstructorControl()
        {
            Focusable = true;
            InitializeComponent();

            _mapControl = new MapControl();


            var pointLayer = CreatePointLayer();
            var editLayer = CreateEditLayer();

            _mapControl.Map = new MapBuilder()
                .SetOpenStreetMapLayer()
                .SetWritableLayer(pointLayer)
                .SetWritableLayer(editLayer)
                .SetTextWidget("Постройте карту")
                .SetCoordinatesWidget()
                .SetScaleBarWidget()
                .SetZoomWidget()
                .SetBoundsFromLonLat(22.0, 34.0, 180, 80.0)
                .SetBoundsLayerFromLonLat(26.9, 40.3, 180, 74.6)
                .SetHome(4337667, 5793728, 50)
                .Build();


            Content = _mapControl;

            _mapManager = new MapConstructorManager(_mapControl);
            _mapManager.InitEditManager();

            //_mapManager.SetEditMode(EditMode.AddPoint);
        }

        private static readonly Color EditModeColor = new Color(124, 22, 111, 180);

        private static readonly Color PointLayerColor = new Color(240, 240, 240, 240);

        private static WritableLayer CreatePointLayer()
        {
            return new WritableLayer
            {
                Name = "PointLayer",
                Style = CreatePointStyle()
            };
        }
        private static WritableLayer CreateEditLayer()
        {
            return new WritableLayer
            {
                Name = "EditLayer",
                Style = CreateEditLayerStyle(),
                IsMapInfoLayer = true
            };
        }

        private static IStyle CreatePointStyle()
        {
            return new VectorStyle
            {
                Fill = new Brush(PointLayerColor),
                Line = new Pen(PointLayerColor, 3),
                Outline = new Pen(Color.Gray, 2)
            };
        }

        private static StyleCollection CreateEditLayerStyle()
        {
            // The edit layer has two styles. That is why it needs to use a StyleCollection.
            // In a future version of Mapsui the ILayer will have a Styles collections just
            // as the GeometryFeature has right now.
            // The first style is the basic style of the features in edit mode.
            // The second style is the way to show a feature is selected.
            return new StyleCollection
            {
                Styles = {
                CreateEditLayerBasicStyle(),
                CreateSelectedStyle()
            }
            };
        }
        private static IStyle CreateEditLayerBasicStyle()
        {
            var editStyle = new VectorStyle
            {
                Fill = new Brush(EditModeColor),
                Line = new Pen(EditModeColor, 3),
                Outline = new Pen(EditModeColor, 3)
            };
            return editStyle;
        }
        private static IStyle CreateSelectedStyle()
        {
            // To show the selected style a ThemeStyle is used which switches on and off the SelectedStyle
            // depending on a "Selected" attribute.
            return new ThemeStyle(f => (bool?)f["Selected"] == true ? SelectedStyle : DisableStyle);
        }

        private static readonly SymbolStyle? SelectedStyle = new SymbolStyle
        {
            Fill = null,
            Outline = new Pen(Color.Red, 3),
            Line = new Pen(Color.Red, 3)
        };

        private static readonly SymbolStyle? DisableStyle = new SymbolStyle { Enabled = false };







    }
}
