using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Styles;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using ReactiveUI;
using RouteOptimization.Controls;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Repository.SQLite;
using RouteOptimization.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Location = RouteOptimization.Models.Entities.Location;

namespace RouteOptimization.Models
{
    public class MapBuilderModel
    {
        private IRepository<Location> _locationsRepository;
        private IRepository<Route> _routesRepository;

        private WritableLayer? _pointLayer;
        private WritableLayer? _lineLayer;

        public MapBuilderModel(IRepository<Location> locationsRepository, IRepository<Route> routesRepository)
        {
            _locationsRepository = locationsRepository;
            _routesRepository = routesRepository;
        }

        public async Task<Map> GetMap()
        {
            var map = await Task.Run(CreateMap);

            _pointLayer?.AddRange(await GetPointFeatures());
            _lineLayer?.AddRange(await GetLineFeatures());

            return map;
        }

        private Map CreateMap()
        {
            _lineLayer = MapsuiTools.CreateLayer("LineLayer");
            _pointLayer = MapsuiTools.CreateLayer("PointLayer");

            return new MapBuilder()
                .SetOpenStreetMapLayer()
                .SetWritableLayer(_lineLayer)
                .SetWritableLayer(_pointLayer)
                .SetTextWidget("Просмотр карты")
                .SetCoordinatesWidget()
                .SetScaleBarWidget()
                .SetBoundsFromLonLat(22.0, 34.0, 180, 80.0)
                .SetBoundsLayerFromLonLat(26.9, 40.3, 180, 74.6)
                .SetHome(4337667, 5793728, 50)
                .Build();
        }

        private async Task<IEnumerable<IFeature>> GetPointFeatures()
        {
            var locations = new List<Location?>(await _locationsRepository.GetAll());

            var features = new List<IFeature>();
            foreach (var location in locations)
            {
                if (location == null) continue;

                var labelStyle = MapsuiTools.CreateLabelStyle(location.Name ?? "Без имени");
                var symbolStyle = MapsuiTools.CreateSymbolStyle();

                var point = MapsuiTools.CreatePointFeature(location.X, location.Y, symbolStyle, labelStyle);

                features.Add(point);
            }

            return features;
        }

        private async Task<IEnumerable<IFeature>> GetLineFeatures()
        {
            var routes = new List<Route?>(await _routesRepository.GetAll());

            var features = new List<IFeature>();
            foreach (var route in routes)
            {
                if (route == null) continue;

                if (route.StartLocation != null && route.FinishLocation != null)
                {
                    var vectorStyle = MapsuiTools.CreateVectorStyle();

                    var geometry = MapsuiTools.GetGeometry(route.StartLocation, route.FinishLocation);

                    var geometryFeature = MapsuiTools.CreateGeometryFeature(geometry, vectorStyle);

                    features.Add(geometryFeature);
                }
            }

            return features;
        }
    }
}
