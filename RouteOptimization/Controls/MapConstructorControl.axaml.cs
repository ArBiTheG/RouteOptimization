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

            Content = _mapControl;

            _mapManager = new MapConstructorManager(_mapControl);

        }

    }
}
