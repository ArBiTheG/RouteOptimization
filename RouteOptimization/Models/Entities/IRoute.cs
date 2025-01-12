using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public interface IRoute
    {
        int Id { get; }
        Location? StartLocation { get; set; }
        Location? FinishLocation { get; set; }
        float Distance { get; set; }
        float Time { get; set; }
    }
}
