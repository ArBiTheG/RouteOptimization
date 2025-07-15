using ReactiveUI;
using RouteOptimization.ViewModels.Pages.DataViewers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class DatabaseViewModel: ViewModelBase
    {
        private PageItem[] PageItems { get; } =
        {
            new PageItem("Locations", typeof(LocationsViewModel)),
            new PageItem("Routes", typeof(RoutesViewModel)),
            new PageItem("Cargo", typeof(CargosViewModel)),
            new PageItem("Shipments", typeof(ShipmentsViewModel)),
            new PageItem("Vehicles", typeof(VehiclesViewModel)),
            new PageItem("Settings", typeof(SettingsDatabaseViewModel)),
        };

        private HistoryRouter<ViewModelBase> _router;
        public ReactiveCommand<string, Unit> OpenPage { get; }

        public DatabaseViewModel() { }

        public DatabaseViewModel(HistoryRouter<ViewModelBase> router)
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
