using RouteOptimization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class LoadingViewModel : ViewModelBase
    {
        LoadingModel _model;

        public LoadingViewModel(LoadingModel model)
        {
            _model = model;
        }
    }
}
