using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public interface IRoute
    {
        int Id { get; set; }
        double Distance { get; set; }
        int BeginId { get; set; }
        IOffice Begin { get; set; }
        int EndId { get; set; }
        IOffice End { get; set; }
    }
}
