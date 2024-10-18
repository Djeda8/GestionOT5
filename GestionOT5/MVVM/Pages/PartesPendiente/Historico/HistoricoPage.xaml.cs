using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.OtsPendientes;

namespace GestionOT5.MVVM.Pages.PartesPendiente.Historico;

public partial class HistoricoPage : ContentPage
{
	public HistoricoPage(OtsPendientesViewModel vm)
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