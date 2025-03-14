using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Mobile.ViewModels;

public partial class CreateNoteViewModel : ObservableObject
{
    [ObservableProperty]
    private string _content = string.Empty;

    [RelayCommand]
    private Task SaveNote()
    {
        return Task.CompletedTask;
    }
}
