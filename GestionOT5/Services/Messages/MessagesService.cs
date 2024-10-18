using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace GestionOT5.Services.Messages
{
    public class MessagesService : IMessagesService
    {
        public async Task ShowCustomToast(string? message)
        {
            var toast = Toast.Make(message is null ? "Mensaje sin determinar" : message, ToastDuration.Long);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            await toast.Show(cts.Token);

            cts.Dispose();
        }
    }
}
