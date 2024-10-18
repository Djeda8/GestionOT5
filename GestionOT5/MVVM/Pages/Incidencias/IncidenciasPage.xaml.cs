using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.Incidencias;

namespace GestionOT5.MVVM.Pages.Incidencias;

public partial class IncidenciasPage : ContentPage
{
	public IncidenciasPage(IncidenciasViewModel vm)
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