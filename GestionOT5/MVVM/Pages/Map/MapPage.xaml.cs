using GestionOT5.MVVM.ViewModels;
using GestionOT5.MVVM.ViewModels.Map;

namespace GestionOT5.MVVM.Pages.Map;

public partial class MapPage : ContentPage
{
	public MapPage(MapViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    protected override void OnAppearing()
    {
        var bindingContext = BindingContext as BaseViewModel;
        if (bindingContext != null)
        {
            bindingContext.MyMap = map;
            bindingContext.OnAppearing();
        }
    }

    protected override void OnDisappearing()
    {
        var bindingContext = BindingContext as BaseViewModel;
        bindingContext?.OnDisappearing();
    }
}