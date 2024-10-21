using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteOptimization.ViewModels.Pages.DataEditors
{
    public class DeleteViewModel : ViewModelBase
    {
        public ReactiveCommand<bool, object> ApplyCommand { get; }

        public DeleteViewModel()
        {
            ApplyCommand = ReactiveCommand.Create<bool, object>(ExecuteApplyCommand);
        }

        private object ExecuteApplyCommand(bool arg)
        {
            return arg;
        }
    }
}
