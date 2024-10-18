using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.PartesEdicion;

namespace GestionOT5.MVVM.Pages.PartesEdicion.Materiales;

public partial class MaterialesPage : ContentPage
{
    public MaterialesPage(PartesEdicionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        var bindingContext = BindingContext as BaseViewModel;

        if (bindingContext != null)
        {
            bindingContext.MyEntry = cantidad;
            bindingContext.OnAppearing();
        }
    }

    protected override void OnDisappearing()
    {
        var bindingContext = BindingContext as BaseViewModel;
        bindingContext?.OnDisappearing();
    }
}