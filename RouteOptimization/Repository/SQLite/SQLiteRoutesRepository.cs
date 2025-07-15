using DynamicData;
using Microsoft.EntityFrameworkCore;
using RouteOptimization.Library.Builder;
using RouteOptimization.Library.Entity;
using RouteOptimization.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Repository.SQLite
{
    public class SQLiteRoutesRepository : IRoutesRepository
    {
        public async Task<Route?> Create(Route entity)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Route entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Routes.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(Route entity)
        {
            using SQLiteContext context = new SQLiteContext();
            context.Routes.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Route?>> GetAll()
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.Include(l => l.StartLocation).Include(l => l.FinishLocation).LoadAsync();
            return context.Routes.Local.ToArray();
        }

        public async Task<IEnumerable<Route?>> GetAll(int page, int pageSize = 10, string filter = "")
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Routes.Skip((page - 1) * pageSize).Take(pageSize).LoadAsync();
            return context.Routes.ToArray();
        }

        public async Task<Route?> GetByID(int id)
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Routes.Include(l => l.StartLocation).Include(l => l.FinishLocation).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int?> Count()
        {
            using SQLiteContext context = new SQLiteContext();
            return await context.Routes.CountAsync();
        }

        public async Task<RouteWay> GetRouteWay(int startLocationId, int finishLocationId)
        {
            using SQLiteContext context = new SQLiteContext();
            await context.Locations.LoadAsync();
            await context.Routes.LoadAsync();

            Route[] routesContext = await context.Routes.ToArrayAsync();
            Location[] locationsContext = await context.Locations.ToArrayAsync();

            IGraphBuilder graphBuider = GraphBuilder.Create();
            foreach (var route in routesContext)
                if (route != null)
                    graphBuider.AddEdge(route.StartLocationId, route.FinishLocationId, route.Time);
            Graph _graph = await graphBuider.BuildAsync();


            Way way = WayBuilder.Create(_graph).SetBegin(startLocationId).SetEnd(finishLocationId).Build();


            HashSet<Route> routes = new HashSet<Route>();
            HashSet<Location> locations = new HashSet<Location>();

            var vertexList = way.Vertices.ToList();
            for (int i = 0;i < vertexList.Count(); i++)
            {
                var vertex = vertexList[i];

                if (i + 1 < vertexList.Count())
                {
                    var vertexNext = vertexList[i + 1];
                    var route = routesContext.First(r => r.StartLocationId == vertex.Id && r.FinishLocationId == vertexNext.Id ||
                        r.StartLocationId == vertexNext.Id && r.FinishLocationId == vertex.Id);
                    if (route != null)
                    {
                        routes.Add(route);
                    }
                }

                var location = locationsContext.First(l => l.Id == vertex.Id);
                if (location != null)
                {
                    locations.Add(location);
                }
            }

            RouteWay routeWay = new RouteWay(routes, locations, way.Weight);

            return routeWay;
        }

    }
}
