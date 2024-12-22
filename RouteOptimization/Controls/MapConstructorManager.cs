using Avalonia.Media;
using Mapsui.Layers;
using Mapsui;
using Mapsui.Nts.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapsui.UI;
using Mapsui.UI.Avalonia;
using Mapsui.Extensions;
using ReactiveUI;
using Avalonia.Controls;
using Mapsui.Nts.Widgets;

namespace RouteOptimization.Controls
{
    public class MapConstructorManager : ReactiveObject
    {
        IMapControl _mapControl;
        EditManager _editManager;
        EditManipulation _editManipulation;

        public MapConstructorManager(IMapControl mapControl)
        {
            _mapControl = mapControl;
        }

        public void InitEditManager()
        {
            var map = _mapControl.Map;

            _editManager = new EditManager
            {
                Layer = (WritableLayer)map.Layers.First(l => l.Name == "EditLayer")
            };
            var tempLayer = (WritableLayer)map.Layers.First(l => l.Name == "PointLayer");

            // Load the polygon layer on startup so you can start modifying right away
            _editManager.Layer.AddRange(tempLayer.GetFeatures().Copy());
            tempLayer.Clear();
            _editManager.EditMode = EditMode.Modify;

            _editManipulation = new EditManipulation();

            map.Widgets.Add(new EditingWidget(_mapControl, _editManager, _editManipulation));
        }


        public void SetEditMode(EditMode editMode)
        {
            _editManager.EditMode = editMode;
        }

    }
}
