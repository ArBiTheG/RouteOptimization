using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public interface ILocation
    {
        int Id { get; }
        string? Name { get; set; }
        string? Description { get; set; }
        float X { get; set; }
        float Y { get; set; }
    }
}
