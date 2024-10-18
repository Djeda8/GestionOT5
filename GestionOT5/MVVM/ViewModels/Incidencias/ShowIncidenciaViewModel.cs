using CommunityToolkit.Mvvm.ComponentModel;
using GestionOT5.MVVM.Models;
using GestionOT5.Services.Messages;
using GestionOT5.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.MVVM.ViewModels.Incidencias
{
    public partial class ShowIncidenciaViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        private Incidencia? itemIncidencia;

        public ShowIncidenciaViewModel(IMessagesService messagesService, INavigationService navigationService) : base(messagesService, navigationService)
        {
            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("incidencia"))
            {
                ItemIncidencia = query["incidencia"] as Incidencia;
            }
        }
    }
}
