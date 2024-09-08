using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models
{
    public class Route : IRoute
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Distance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int BeginId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IOffice Begin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int EndId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IOffice End { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
