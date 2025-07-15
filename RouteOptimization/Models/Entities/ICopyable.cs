using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.Models.Entities
{
    public interface ICopyable<T>
    {
        void CopyFrom(T entity);
    }
}
