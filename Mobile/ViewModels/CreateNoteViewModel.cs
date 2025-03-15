﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobile.Constants;
using Mobile.Services;

namespace Mobile.ViewModels;

public partial class CreateNoteViewModel : ObservableObject
{
    private readonly IApiClient _apiClient;
    private readonly IErrorHandler _errorHandler;

    [ObservableProperty]
    private string _state = States.DEFAULT;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveNoteCommand))]
    private bool _canStateChange;

    [ObservableProperty]
    private string _content = string.Empty;

    public CreateNoteViewModel(IApiClient apiClient, IErrorHandler errorHandler)
    {
        _apiClient = apiClient;
        _errorHandler = errorHandler;
    }

    [RelayCommand(CanExecute = nameof(CanStateChange))]
    private async Task SaveNote()
    {
        try
        {
            State = States.BUSY;
            await _apiClient.CreateNote(Content);
        }
        catch (Exception ex)
        {
            _errorHandler.HandleError(ex);
        }
        finally
        {
            State = States.DEFAULT;
        }
    }
}
