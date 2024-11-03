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
        int FinishLocationId { get; set; }
        Location? FinishLocation { get; set; }
        float Distance { get; set; }
        float Time { get; set; }
    }
}
