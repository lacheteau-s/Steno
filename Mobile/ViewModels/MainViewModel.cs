using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Constants;
using Mobile.Services;
using System.Collections.ObjectModel;

namespace Mobile.ViewModels;

public partial class MainViewModel : ObservableObject, IQueryAttributable
{
    private readonly IApiClient _apiClient;
    private readonly IErrorHandler _errorHandler;

    [ObservableProperty]
    private ObservableCollection<NoteViewModel> _notes = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GetNotesCommand))]
    private bool _shouldRefresh = true;

    [ObservableProperty]
    private string _state = States.DEFAULT;

    public MainViewModel(IApiClient apiClient, IErrorHandler errorHandler)
    {
        _apiClient = apiClient;
        _errorHandler = errorHandler;
    }

    [RelayCommand]
    private Task CreateNote() => Shell.Current.GoToAsync("CreateNote");

    [RelayCommand(CanExecute = nameof(ShouldRefresh))]
    private async Task GetNotes()
    {
        try
        {
            State = States.BUSY;
            var notes = await _apiClient.GetNotes();
            Notes = [.. notes.Select(x => new NoteViewModel(x))];
            State = States.DEFAULT;
        }
        catch (Exception ex)
        {
            State = States.ERROR;
            _errorHandler.HandleError(ex);
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(QueryParameters.CreatedNote, out var createdNote))
        {
            Notes = [.. Notes.Prepend((NoteViewModel)createdNote)];
            ShouldRefresh = false;
        }
    }
}
