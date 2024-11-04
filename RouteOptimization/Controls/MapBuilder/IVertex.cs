using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Controls.MapBuilder
{
    public interface IVertex: IScenePoint
    {
        float X { get; set; }
        float Y { get; set; }
        float Size { get; set; }
    }
}
