namespace Commons.Debugging.SettingsEditor.Interfaces
{
    public interface IEnumerateAllParams
    {
        void OnBool(string name, bool value);
        void OnInt(string name, int value, int min, int max);
        void OnFloat(string name, float value, float min, float max, bool wholeNumbers);
    }
}