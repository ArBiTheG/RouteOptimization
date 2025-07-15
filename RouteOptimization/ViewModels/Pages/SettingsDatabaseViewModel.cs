using ReactiveUI;
using RouteOptimization.ViewModels.Pages.DataViewers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class SettingsDatabaseViewModel: ViewModelBase
    {
        private PageItem[] PageItems { get; } =
        {
            new PageItem("CargoAvailables", typeof(CargoAvailablesViewModel)),
            new PageItem("ShipmentStatuses", typeof(ShipmentStatusesViewModel)),
            new PageItem("VehicleStatuses", typeof(VehicleStatusesViewModel)),
            new PageItem("VehicleTypes", typeof(VehicleTypesViewModel)),
        };

        private HistoryRouter<ViewModelBase> _router;
        public ReactiveCommand<string, Unit> OpenPage { get; }

        public SettingsDatabaseViewModel() { }

        public SettingsDatabaseViewModel(HistoryRouter<ViewModelBase> router)
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
