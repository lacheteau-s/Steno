using Mobile.ViewModels;
using CommunityToolkit.Maui.Core.Platform;

namespace Mobile.Views;

public partial class CreateNoteView : ContentPage
{
	public CreateNoteView(CreateNoteViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

    private async void OnSave(object sender, EventArgs e)
    {
		// TODO: find a better way to handle this
        await Editor.HideKeyboardAsync();
    }
}