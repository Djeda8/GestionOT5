using CommunityToolkit.Mvvm.ComponentModel;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using Microsoft.Maui.Controls.Maps;

namespace GestionOT5.MVVM.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public Entry MyEntry;
        public Microsoft.Maui.Controls.Maps.Map MyMap;

        protected readonly IMessagesService _messagesService;
        protected readonly INavigationService _navigationService;
        public BaseViewModel(IMessagesService messagesService, INavigationService navigationService)
        {
            _messagesService = messagesService;
            _navigationService = navigationService;
        }

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool _title;

        public virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }

        public virtual Task OnDisappearing()
        {
            return Task.CompletedTask;
        }
    }
}
