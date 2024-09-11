using ReactiveUI;
using RouteOptimization.ViewModels.Pages.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages
{
    public class HomePageViewModel: ViewModelBase
    {
        private PageItem[] PageItems { get; } =
        {
            new PageItem("Handle", typeof(HandleDataPageViewModel)),
            new PageItem("Builder", typeof(MapBuilderPageViewModel)),
            new PageItem("Route", typeof(MapRoutePageViewModel)),
        };

        private HistoryRouter<ViewModelBase> _router;
        public ReactiveCommand<string, Unit> OpenPage { get; }


        public HomePageViewModel(HistoryRouter<ViewModelBase> router)
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
