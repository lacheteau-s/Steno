using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Services;

namespace Mobile.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IApiClient _apiClient;

    [ObservableProperty]
    private IEnumerable<NoteViewModel> _notes = [];

    public MainViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [RelayCommand]
    private Task CreateNote() => Shell.Current.GoToAsync("CreateNote");

    [RelayCommand]
    private async Task GetNotes()
    {
        var notes = await _apiClient.GetNotes();
        Notes = notes.Select(x => new NoteViewModel(x));
    }
}
