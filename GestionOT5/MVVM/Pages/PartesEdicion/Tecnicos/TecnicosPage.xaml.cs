using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.PartesEdicion;

namespace GestionOT5.MVVM.Pages.PartesEdicion.Tecnicos;

public partial class TecnicosPage : ContentPage
{
	public TecnicosPage(PartesEdicionViewModel vm)
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