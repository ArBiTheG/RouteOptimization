using RouteOptimization.WpfApp.Core;
using RouteOptimization.WpfApp.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.WpfApp.ViewModel
{
    public class HomeVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public RelayCommand ChangePageCommand { get; }
        public HomeVM()
        {
        }
        public HomeVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            ChangePageCommand = new RelayCommand(ExecuteChangePageCommand);
        }
        public void ExecuteChangePageCommand(object? parameter)
        {
            if (_navigationStore == null) return;
            string? pageName = parameter as string;
            if (pageName != null)
            {
                switch (pageName)
                {
                    case "Database":
                        _navigationStore.CurrentViewModel = new DatabaseVM(_navigationStore);
                        break;
                    case "MapBuilder":
                        _navigationStore.CurrentViewModel = new MapBuilderVM();
                        break;
                    case "MapRouter":
                        _navigationStore.CurrentViewModel = new MapRouterVM();
                        break;
                }
            }
        }
    }
}
