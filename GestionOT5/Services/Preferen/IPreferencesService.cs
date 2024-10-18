using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Preferen
{
    public interface IPreferencesService
    {
        bool ContainsKey(string v);
        string GetValue(string key, string value);
        void SetValue(string key, string value);
        void RemoveKey(string key);
    }
}