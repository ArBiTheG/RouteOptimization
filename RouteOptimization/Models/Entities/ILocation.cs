using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public interface ILocation
    {
        string? Name { get; set; }
        float X { get; set; }
        float Y { get; set; }
    }
}
