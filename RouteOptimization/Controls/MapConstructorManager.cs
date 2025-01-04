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
using Avalonia.Markup.Xaml.Templates;
using Mapsui.Styles.Thematics;
using Mapsui.Styles;
using Mapsui.Widgets.ButtonWidget;
using Mapsui.Widgets;

namespace RouteOptimization.Controls
{
    public class MapConstructorManager : ReactiveObject
    {
        private EditManager _editManager = new();

        private IMapControl _mapControl;


        /// <summary>
        /// Заблокированный слой
        /// </summary>
        WritableLayer? _lockLayer;



        /// <summary>
        /// Временные свойство, используются для отмены изменений
        /// </summary>
        private List<IFeature>? _oldFeatures;



        public MapConstructorManager(IMapControl mapControl)
        {
            _mapControl = mapControl;
            Map map = CreateMap();

            _lockLayer = map.Layers.FirstOrDefault(f => f.Name == "PointLayer") as WritableLayer;

            InitEditWidgets(map);


            _editManager = new EditManager
            {
                Layer = (WritableLayer)map.Layers.First(l => l.Name == "EditLayer")
            };


            EditManipulation editManipulation = new EditManipulation();

            map.Widgets.Add(new EditingWidget(_mapControl, _editManager, editManipulation));
            _mapControl.Map = map;

            _editManager.EditMode = EditMode.None;
        }

        /// <summary>
        /// Заблокировать изменения
        /// </summary>
        private void LockChanges()
        {
            _lockLayer?.AddRange(_editManager.Layer?.GetFeatures().Copy() ?? new List<IFeature>());
            _editManager.Layer?.Clear();

            _mapControl?.RefreshGraphics();

            _oldFeatures = null;
        }

        /// <summary>
        /// Разблокировать слой
        /// </summary>
        private void UnlockLayer()
        {
            var features = _lockLayer?.GetFeatures().Copy() ?? Array.Empty<IFeature>();

            foreach (var feature in features)
            {
                feature.RenderedGeometry.Clear();
            }

            _editManager.Layer?.AddRange(features);
            _lockLayer?.Clear();

            _mapControl?.RefreshGraphics();
        }

        /// <summary>
        /// Загрузить старые изменения
        /// </summary>
        private void LoadOldFeatures()
        {
            if (_lockLayer != null && _oldFeatures != null)
            {
                _lockLayer.Clear();
                _lockLayer.AddRange(_oldFeatures.Copy());
            }

            _editManager.Layer?.Clear();
            _mapControl?.RefreshGraphics();

            _oldFeatures = null;

            _editManager.EditMode = EditMode.None;
        }

        /// <summary>
        /// Задать старые изменения
        /// </summary>
        private void SetOldFeature()
        {
            var features = _lockLayer?.GetFeatures().Copy() ?? Array.Empty<IFeature>();

            foreach (var feature in features)
            {
                feature.RenderedGeometry.Clear();
            }

            _oldFeatures = new List<IFeature>(features);
        }

        private void SetEditMode(EditMode editMode)
        {
            LockChanges();
            SetOldFeature();
            switch (editMode)
            {
                case EditMode.AddPoint:
                    break;
                case EditMode.Modify:
                    UnlockLayer();

                    break;
                default:
                    break;
            }

            _editManager.EditMode = editMode;
        }

        public void Save()
        {

        }
        public void Load()
        {

        }

        public void DoApply()
        {
            SetEditMode(EditMode.None);
        }
        public void DoCancel()
        {
            LoadOldFeatures();
        }

        private static Map CreateMap()
        {
            var pointLayer = CreatePointLayer();
            var editLayer = CreateEditLayer();

            return new MapBuilder()
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
        }


        public void InitEditWidgets(Map map)
        {
            var apply = new ButtonWidget
            {
                MarginY = 120,
                MarginX = 5,
                Height = 18,
                Width = 120,
                CornerRadius = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Применить",
                BackColor = Color.LightGray,
            };
            apply.WidgetTouched += (_, e) =>
            {
                DoApply();
                e.Handled = true;
            };
            map.Widgets.Add(apply);

            var cancel = new ButtonWidget
            {
                MarginY = 140,
                MarginX = 5,
                Height = 18,
                Width = 120,
                CornerRadius = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Отмена",
                BackColor = Color.LightGray,
            };
            cancel.WidgetTouched += (_, e) =>
            {
                DoCancel();
                e.Handled = true;
            };
            map.Widgets.Add(cancel);

            var addPoint = new ButtonWidget
            {
                MarginY = 80,
                MarginX = 5,
                Height = 18,
                Width = 120,
                CornerRadius = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Добавить метку",
                BackColor = Color.LightGray,
            }; 
            addPoint.WidgetTouched += (_, e) =>
            {
                SetEditMode(EditMode.AddPoint);
                e.Handled = true;
            };
            map.Widgets.Add(addPoint);

            var editPoint = new ButtonWidget
            {
                MarginY = 100,
                MarginX = 5,
                Height = 18,
                Width = 120,
                CornerRadius = 2,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Редактировать метку",
                BackColor = Color.LightGray,
            };
            editPoint.WidgetTouched += (_, e) =>
            {
                SetEditMode(EditMode.Modify);
                e.Handled = true;
            };
            map.Widgets.Add(editPoint);
        }




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

        private static readonly Color EditModeColor = new Color(124, 22, 111, 180);

        private static readonly Color TargetLayerColor = new Color(240, 240, 240, 240);

        private static IStyle CreatePointStyle()
        {
            return new VectorStyle
            {
                Fill = new Brush(TargetLayerColor),
                Line = new Pen(TargetLayerColor, 3),
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
