using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Models;
using Mobile.Services;

namespace Mobile.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IApiClient _apiClient;

    [ObservableProperty]
    private IEnumerable<NoteModel> _notes = [];

    public MainViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [RelayCommand]
    private Task CreateNote() => Shell.Current.GoToAsync("CreateNote");

    [RelayCommand]
    private async Task GetNotes()
    {
        Notes = await _apiClient.GetNotes();
    }
}
