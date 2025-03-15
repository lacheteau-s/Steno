using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Services;

namespace Mobile.ViewModels;

public partial class CreateNoteViewModel : ObservableObject
{
    private readonly IApiClient _apiClient;

    [ObservableProperty]
    private string _content = string.Empty;

    public CreateNoteViewModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [RelayCommand]
    private async Task SaveNote()
    {
        await _apiClient.CreateNote(Content);
    }
}
