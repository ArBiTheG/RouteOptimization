using ReactiveUI;
using RouteOptimization.Controls.MapBuilder;
using RouteOptimization.ViewModels.Pages.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class HandleDataPageViewModel: ViewModelBase
    {
        private PageItem[] PageItems { get; } =
        {
            new PageItem("Locations", typeof(LocationsPageViewModel)),
            new PageItem("Routes", typeof(RoutesPageViewModel)),
            new PageItem("Shipments", typeof(ShipmentsPageViewModel)),
            new PageItem("Vehicles", typeof(VehiclesPageViewModel)),
            new PageItem("VehicleStatuses", typeof(VehicleStatusesPageViewModel)),
            new PageItem("VehicleTypes", typeof(VehicleTypesPageViewModel)),
        };

        private HistoryRouter<ViewModelBase> _router;
        public ReactiveCommand<string, Unit> OpenPage { get; }


        public HandleDataPageViewModel(HistoryRouter<ViewModelBase> router)
        {
            _router = router;

            OpenPage = ReactiveCommand.Create<string>(ExecuteOpenPage);
        }

        private void ExecuteOpenPage(string pageName)
        {
            foreach (var item in PageItems)
            {
                if (item.Name == pageName)
                {
                    _router.GoTo(item.ModelType);
                }
            }
        }
    }
}
