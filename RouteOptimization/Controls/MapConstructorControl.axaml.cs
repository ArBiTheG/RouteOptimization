using Avalonia.Controls;
using Mapsui.Layers;
using Mapsui.Nts.Editing;
using Mapsui.Styles.Thematics;
using Mapsui.Styles;
using Mapsui.UI;
using Mapsui.UI.Avalonia;
using Mapsui;
using Avalonia;

namespace RouteOptimization.Controls
{
    public partial class MapConstructorControl : UserControl
    {
        MapControl _mapControl;

        public MapConstructorControl()
        {
            Focusable = true;

            _mapControl = new MapControl(); 

            Content = _mapControl;
        }

        public static readonly DirectProperty<MapConstructorControl, Map?> MapSourceProperty =
            AvaloniaProperty.RegisterDirect<MapConstructorControl, Map?>(
                nameof(MapSource),
                o => o._mapControl.Map,
                (o, v) => o._mapControl.Map = v ?? new Map());

        public Map? MapSource
        {
            get
            {
                return GetValue(MapSourceProperty);
            }
            set { 
                SetValue(MapSourceProperty, value);
            }
        }

    }
}
