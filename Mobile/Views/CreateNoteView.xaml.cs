using Mobile.ViewModels;

namespace Mobile.Views;

public partial class CreateNoteView : ContentPage
{
	public CreateNoteView(CreateNoteViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}