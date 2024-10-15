using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public interface IRoute
    {
        int Id { get; }
        int StartLocationId { get; set; }
        Location? StartLocation { get; set; }
        int EndLocationId { get; set; }
        Location? EndLocation { get; set; }
        double Distance { get; set; }
        double Time { get; set; }
    }
}
