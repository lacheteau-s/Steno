
using System.Threading.Tasks;

namespace Mobile.Services;

public class AlertErrorHandler : IErrorHandler
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async void HandleError(Exception ex)
    {
        try
        {
            await DisplayAlert(ex);
        }
        catch { }
    }

    private async Task DisplayAlert(Exception ex)
    {
        try
        {
            await _semaphore.WaitAsync();

            if (Shell.Current is Shell shell)
                await shell.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
