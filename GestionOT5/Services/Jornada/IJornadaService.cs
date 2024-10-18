





namespace GestionOT5.Services.Jornada
{
    public interface IJornadaService
    {
        void CreateJornada(DateTime time);
        void DeleteJornada();
        bool ExistsJornada();
        MVVM.Models.Jornada? GetJornada();
        string MessageJornada();
    }
}