using Avalonia.Controls;
using Mapsui.Layers;
using Mapsui.Nts.Editing;
using Mapsui.Styles.Thematics;
using Mapsui.Styles;
using Mapsui.UI;
using Mapsui.UI.Avalonia;
using Mapsui;
using Avalonia;
using System.Diagnostics;

namespace RouteOptimization.Controls
{
    public partial class MapConstructorControl : MapControl
    {

        public MapConstructorControl()
        {
            Focusable = true;

        }

        public static readonly DirectProperty<MapConstructorControl, Map?> MapSourceProperty =
            AvaloniaProperty.RegisterDirect<MapConstructorControl, Map?>(
                nameof(MapSource),
                o => o.Map,
                (o, v) => o.Map = v ?? new Map());

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
