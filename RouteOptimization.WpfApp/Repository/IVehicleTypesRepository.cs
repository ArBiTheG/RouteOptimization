﻿using RouteOptimization.WpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.Repository
{
    public interface IVehicleTypesRepository
    {
        Task<IEnumerable<VehicleType?>> GetAll();
        Task<VehicleType?> GetByID(int id);
        Task<VehicleType?> Create(VehicleType entity);
        Task Edit(VehicleType entity);
        Task Delete(VehicleType entity);
    }
}
