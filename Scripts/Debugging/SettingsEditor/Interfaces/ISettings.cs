namespace Commons.Debugging.SettingsEditor.Interfaces
{
    public interface ISettings
    {
        void Load();
        void Save();
        void Invalidate();
    }
}