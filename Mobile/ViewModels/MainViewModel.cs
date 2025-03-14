using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Mobile.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CounterText))]
    private int _count = 0;

    public string CounterText => Count == 0 ? "Click me" : $"Clicked {Count} time{(Count > 1 ? "s" : "")}";

    [RelayCommand]
    public void IncrementCounter()
    {
        Count++;

        SemanticScreenReader.Announce(CounterText);
    }
}
