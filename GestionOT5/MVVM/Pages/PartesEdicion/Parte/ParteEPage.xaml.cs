using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.PartesEdicion;

namespace GestionOT5.MVVM.Pages.PartesEdicion.Parte;

public partial class ParteEPage : ContentPage
{
	public ParteEPage(PartesEdicionViewModel vm)
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