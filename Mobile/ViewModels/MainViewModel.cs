using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Constants;
using Mobile.Services;

namespace Mobile.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IApiClient _apiClient;
    private readonly IErrorHandler _errorHandler;

    [ObservableProperty]
    private IEnumerable<NoteViewModel> _notes = [];

    [ObservableProperty]
    private string _state = States.DEFAULT;

    public MainViewModel(IApiClient apiClient, IErrorHandler errorHandler)
    {
        _apiClient = apiClient;
        _errorHandler = errorHandler;
    }

    [RelayCommand]
    private Task CreateNote() => Shell.Current.GoToAsync("CreateNote");

    [RelayCommand]
    private async Task GetNotes()
    {
        try
        {
            State = States.BUSY;
            var notes = await _apiClient.GetNotes();
            Notes = notes.Select(x => new NoteViewModel(x));
            State = States.DEFAULT;
        }
        catch (Exception ex)
        {
            State = States.ERROR;
            _errorHandler.HandleError(ex);
        }
    }
}
