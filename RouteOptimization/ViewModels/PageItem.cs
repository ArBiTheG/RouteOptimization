using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels
{
    public class PageItem
    {
        public string Name { get; }
        public Type ModelType { get; }
        public PageItem(string name, Type type)
        {
            ModelType = type;
            Name = name;
        }
    }
}
