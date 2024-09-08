using ReactiveUI;

namespace RouteOptimization.ViewModels;

public class ViewModelBase : ReactiveObject
{
    private ViewModelBase? _parent;
    public ViewModelBase? Parent
    {
        get => _parent;
        set => this.RaiseAndSetIfChanged(ref _parent, value);
    }
}
