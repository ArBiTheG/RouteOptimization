using Mapsui;
using Mapsui.Layers;
using RouteOptimization.Controls;
using RouteOptimization.Library.Builder;
using Routing = RouteOptimization.Library.Entity.Route;
using Graph = RouteOptimization.Library.Entity.Graph;
using Vertex = RouteOptimization.Library.Entity.Vertex;
using RouteOptimization.Models.Entities;
using RouteOptimization.Repository;
using RouteOptimization.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapsui.Styles;

namespace RouteOptimization.Models
{
    public class MapRouteModel
    {
        private IRepository<Location> _locationsRepository;
        private IRepository<Route> _routesRepository;

        private Map _map;

        private WritableLayer? _pointLayer;
        private WritableLayer? _lineLayer;

        public MapRouteModel(IRepository<Location> locationsRepository, IRepository<Route> routesRepository)
        {
            _locationsRepository = locationsRepository;
            _routesRepository = routesRepository;

            _map = CreateMap();
        }


        public async Task<Map> GetMap()
        {
            _pointLayer?.Clear();
            _pointLayer?.AddRange(await GetPointFeatures());

            _lineLayer?.Clear();
            _lineLayer?.AddRange(await GetLineFeatures());

            return _map;
        }

        public async Task<IEnumerable<Location?>> GetLocations()
        {
            return await _locationsRepository.GetAll();
        }


        public async Task<Map> Navigate(Location? startLocation, Location? finishLocation)
        {
            var locations = new List<Location?>(await _locationsRepository.GetAll());
            var routes = new List<Route?>(await _routesRepository.GetAll());

            // Создание карты для маршрута

            IGraphBuilder graphBuider = GraphBuilder.Create();

            foreach (var route in routes)
            {
                if (route != null)
                {
                    var startRoute = route.StartLocationId;
                    var finishRoute = route.FinishLocationId;
                    graphBuider.AddEdge(startRoute, finishRoute, route.Time);
                }
            }

            Graph graph = graphBuider.Build();

            // Маршрутизация

            RouteBuilder? routeBuilder = RouteBuilder.Create(graph);

            if (startLocation != null && finishLocation != null)
            {
                int startId = startLocation.Id;
                int finishId = finishLocation.Id;

                routeBuilder.SetBegin(startId).SetEnd(finishId);
            }

            Routing routing = routeBuilder.Build();
            Vertex[] vertices = routing.Vertices.ToArray();

            // Установка точек на карте
            _pointLayer?.Clear();

            var pointFeatures = new List<IFeature>();
            foreach (var vertex in vertices)
            {
                var location = locations.First(u => u?.Id == vertex.Id);

                if (location != null)
                {
                    var labelStyle = MapsuiTools.CreateLabelStyle(location.Name ?? "Без имени");
                    var symbolStyle = MapsuiTools.CreateSymbolStyle();
                    var point = MapsuiTools.CreatePointFeature(location.X, location.Y, symbolStyle, labelStyle);
                    pointFeatures.Add(point);
                }
            }
            _pointLayer?.AddRange(pointFeatures);


            // Установка линий на карте
            _lineLayer?.Clear();

            var lineFeatures = new List<IFeature>();


            for (int i = 1; i < vertices.Length; i++)
            {
                int startId = vertices[i-1].Id;
                int finishId = vertices[i].Id;

                var route = routes.FirstOrDefault(
                    u => u?.StartLocationId == startId && u?.FinishLocationId == finishId || 
                    u?.FinishLocationId == startId && u?.StartLocationId == finishId
                    );

                if (route != null)
                {
                    if (route.StartLocation!=null && route.FinishLocation != null)
                    {
                        var vectorStyle = MapsuiTools.CreateVectorStyle();
                        var geometry = MapsuiTools.GetGeometry(route.StartLocation, route.FinishLocation);
                        var geometryFeature = MapsuiTools.CreateGeometryFeature(geometry, vectorStyle);

                        lineFeatures.Add(geometryFeature);
                    }
                }

            }

            _lineLayer?.AddRange(lineFeatures);

            return _map;
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
