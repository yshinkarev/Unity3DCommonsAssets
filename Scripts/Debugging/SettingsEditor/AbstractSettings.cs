using Commons.Debugging.SettingsEditor.Interfaces;
using Commons.Reflection;

namespace Commons.Debugging.SettingsEditor
{
    public abstract class AbstractSettings : ISettings
    {
        public abstract void Load();

        public abstract void Save();

        public virtual void Invalidate()
        {

        }

        public override string ToString()
        {
            return ReflectionUtils.PublicVariablesToString(this);
        }
    }
}