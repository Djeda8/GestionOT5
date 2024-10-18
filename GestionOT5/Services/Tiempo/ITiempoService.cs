
namespace GestionOT5.Services.Tiempo
{
    public interface ITiempoService
    {
        bool ExistsTiempo();
        DateTime? GetHoraInicio();
        bool GetTiempoEnMarcha();
        void InicioTiempo(DateTime horaInicio);
    }
}