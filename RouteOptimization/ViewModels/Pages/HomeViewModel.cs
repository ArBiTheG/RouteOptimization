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
    public class HomeViewModel: ViewModelBase
    {
        private PageItem[] PageItems { get; } =
        {
            new PageItem("Handle", typeof(DatabaseViewModel)),
            new PageItem("Builder", typeof(MapBuilderViewModel)),
            new PageItem("Route", typeof(MapRouteViewModel)),
            new PageItem("Loading", typeof(LoadingViewModel)),
            new PageItem("Handling", typeof(HandlingViewModel)),
            new PageItem("Warehouse", typeof(WarehouseViewModel)),
        };

        private HistoryRouter<ViewModelBase> _router;
        public ReactiveCommand<string, Unit> OpenPage { get; }


        public HomeViewModel()
        {
            OpenPage = ReactiveCommand.Create<string>(ExecuteOpenPage);
        }
        public HomeViewModel(HistoryRouter<ViewModelBase> router): this()
        {

            _router = router;
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
