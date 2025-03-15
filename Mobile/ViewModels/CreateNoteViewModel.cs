using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Services;

namespace Mobile.ViewModels;

public partial class CreateNoteViewModel : ObservableObject
{
    private readonly IApiClient _apiClient;
    private readonly IErrorHandler _errorHandler;

    [ObservableProperty]
    private string _content = string.Empty;

    public CreateNoteViewModel(IApiClient apiClient, IErrorHandler errorHandler)
    {
        _apiClient = apiClient;
        _errorHandler = errorHandler;
    }

    [RelayCommand]
    private async Task SaveNote()
    {
        try
        {
            await _apiClient.CreateNote(Content);
        }
        catch (Exception ex)
        {
            _errorHandler.HandleError(ex);
        }
    }
}
