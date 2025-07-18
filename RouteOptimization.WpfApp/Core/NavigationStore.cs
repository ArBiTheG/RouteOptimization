﻿using RouteOptimization.WpfApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RouteOptimization.WpfApp.Core
{
    public class NavigationStore
    {
        public event Action? CurrentViewModelChanged;

        private ViewModelBase? _currentViewModel;
        public ViewModelBase? CurrentViewModel 
        { 
            get => _currentViewModel; 
            set {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            } 
        }
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    
    }
}
