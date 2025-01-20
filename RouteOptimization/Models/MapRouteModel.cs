using Mapsui;
using Mapsui.Layers;
using RouteOptimization.Controls;
using RouteOptimization.Library.Builder;
using RouteOptimization.Library.Entity;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapsui.Styles;
using System.Security.Cryptography.X509Certificates;

namespace RouteOptimization.Models
{
    public class MapRouteModel
    {
        private IRepository<Location> _locationsRepository;
        private IRoutesRepository _routesRepository;

        private Map _map;

        private WritableLayer _pointLayer;
        private WritableLayer _lineLayer;

        public MapRouteModel(IRepository<Location> locationsRepository, IRoutesRepository routesRepository)
        {
            _locationsRepository = locationsRepository;
            _routesRepository = routesRepository;

            _lineLayer = MapsuiTools.CreateLayer("LineLayer");
            _pointLayer = MapsuiTools.CreateLayer("PointLayer");
            _map = CreateMap();
        }


        public Map GetMap()
        {
            return _map;
        }
        public void ClearMap()
        {
            _pointLayer?.Clear();
            _lineLayer?.Clear();
        }

        public async Task<IEnumerable<Location?>> GetLocations()
        {
            return await _locationsRepository.GetAll();
        }

        public async Task<RouteWay?> Navigate(Location? startLocation, Location? finishLocation)
        {
            if (startLocation!=null && finishLocation != null)
            {
                RouteWay routeWay = await _routesRepository.GetRouteWay(startLocation.Id, finishLocation.Id);

                _pointLayer?.Clear();
                _pointLayer?.AddRange(GetPointFeatures(routeWay.Locations));

                _lineLayer?.Clear();
                _lineLayer?.AddRange(GetLineFeatures(routeWay.Routes));
                return routeWay;
            }
            return null;
        }

        private Map CreateMap()
        {
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

        private IEnumerable<IFeature> GetPointFeatures(IEnumerable<Location> locations)
        {
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

        private IEnumerable<IFeature> GetLineFeatures(IEnumerable<Route> routes)
        {
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
