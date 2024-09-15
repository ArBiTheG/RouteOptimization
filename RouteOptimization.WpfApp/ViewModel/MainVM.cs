using RouteOptimization.WpfApp.Core;
using RouteOptimization.WpfApp.Utilties;
using RouteOptimization.WpfApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RouteOptimization.WpfApp.ViewModel
{
    public class MainVM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private ViewModelBase? _content;

        public ViewModelBase? Content
        {
            get
            {
                return _content;
            }
            private set
            {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }
        public RelayCommand ChangePageCommand { get; }

        public MainVM(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _content = new HomeVM(_navigationStore);
            _navigationStore.CurrentViewModelChanged += NavigationViewModelChanged;

            ChangePageCommand = new RelayCommand(ExecuteChangePageCommand);
        }

        private void NavigationViewModelChanged()
        {
            Content = _navigationStore.CurrentViewModel;
        }

        public void ExecuteChangePageCommand(object? parameter)
        {
            string? pageName = parameter as string;
            if (pageName != null)
            {
                switch (pageName)
                {
                    case "Home":
                        Content = new HomeVM(_navigationStore);
                        break;
                    case "Database":
                        Content = new DatabaseVM(_navigationStore);
                        break;
                    case "MapBuilder":
                        Content = new MapBuilderVM();
                        break;
                    case "MapRouter":
                        Content = new MapRouterVM();
                        break;
                }
            }
        }
    }
}
