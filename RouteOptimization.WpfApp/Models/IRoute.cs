using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Models
{
    public interface IRoute
    {
        int Id { get; set; }
        int StartLocationId { get; set; }
        Location? StartLocation { get; set; }
        int EndLocationId { get; set; }
        Location? EndLocation { get; set; }
        double Distance { get; set; }
        double Time { get; set; }
    }
}
