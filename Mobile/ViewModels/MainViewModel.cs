using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Mobile.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    private Task CreateNote() => Shell.Current.GoToAsync("CreateNote");
}
