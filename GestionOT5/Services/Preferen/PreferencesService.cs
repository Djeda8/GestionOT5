using GestionOT5.MVVM.Models;

namespace GestionOT5.Services.Preferen
{
    public class PreferencesService : IPreferencesService
    {
        public void SetValue(string key, string value)
        {
            Preferences.Set(key, value);
        }
        public string GetValue(string key, string value)
        {
            return Preferences.Get(key, value);
        }

        public bool ContainsKey(string key)
        {
            return Preferences.ContainsKey(key);
        }

        public void RemoveKey(string key)
        {
            Preferences.Remove(key);
        }
    }
}
