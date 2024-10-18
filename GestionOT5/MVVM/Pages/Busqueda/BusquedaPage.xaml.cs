using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.Busqueda;

namespace GestionOT5.MVVM.Pages.Busqueda;

public partial class BusquedaPage : ContentPage
{
	public BusquedaPage(BusquedaViewModel vm)
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