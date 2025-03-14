using Mobile.ViewModels;

namespace Mobile.Views;

public partial class MainView : ContentPage
{
    public MainView(MainViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}
