using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.PartesPendiente;

namespace GestionOT5.MVVM.Pages.PartesPendiente.Parte;

public partial class PartePPage : ContentPage
{
	public PartePPage(PartesPendienteViewModel vm)
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