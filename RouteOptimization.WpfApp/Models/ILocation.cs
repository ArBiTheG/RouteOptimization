using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public interface ILocation
    {
        int Id { get; }
        string? Name { get; set; }
        string? Description { get; set; }
        double X { get; set; }
        double Y { get; set; }
    }
}
