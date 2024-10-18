using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.Jornada;

namespace GestionOT5.MVVM.Pages.Jornada;

public partial class JornadasPage : ContentPage
{
	public JornadasPage(JornadasViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        var bindingContext = BindingContext as BaseViewModel;
        bindingContext?.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        var bindingContext = BindingContext as BaseViewModel;
        bindingContext?.OnDisappearing();
    }
}