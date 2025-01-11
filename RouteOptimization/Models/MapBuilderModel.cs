using Mapsui;
using Mapsui.Layers;
using Mapsui.Styles;
using ReactiveUI;
using RouteOptimization.Controls;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class MapBuilderModel
    {
        ILocationsRepository _locationsRepository;

        public MapBuilderModel()
        {
            _locationsRepository = new SQLiteLocationsRepository();
        }

        public async Task<Map> GetMap()
        {
            return await Task.Run(CreateMap);
        }

        private static Map CreateMap()
        {
            var pointLayer = CreatePointLayer();

            return new MapBuilder()
                .SetOpenStreetMapLayer()
                .SetWritableLayer(pointLayer)
                .SetTextWidget("Постройте карту")
                .SetCoordinatesWidget()
                .SetScaleBarWidget()
                .SetBoundsFromLonLat(22.0, 34.0, 180, 80.0)
                .SetBoundsLayerFromLonLat(26.9, 40.3, 180, 74.6)
                .SetHome(4337667, 5793728, 50)
                .Build();
        }
        private static WritableLayer CreatePointLayer()
        {
            return new WritableLayer
            {
                Name = "PointLayer",
                Style = CreatePointStyle()
            };
        }

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

        public async Task<List<IFeature>> GetPointFeatures()
        {
            var locations = new List<Location?>(await _locationsRepository.GetAll());

            var features = new List<IFeature>();
            foreach (var location in locations)
            {
                if (location == null) continue;

                var point = new PointFeature(location.X, location.Y);
                point.Styles.Add(new LabelStyle
                {
                    Text = location.Name,
                    BackColor = new Brush(Color.Gray),
                    HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Center
                });

                features.Add(point);
            }

            return features;
        }
    }
}
