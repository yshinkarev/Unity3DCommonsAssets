using System.Text;
using Commons.Debugging.SettingsEditor.Interfaces;
using Commons.Interfaces;
using Commons.Utils;
using UnityEngine;
using PP = UnityEngine.PlayerPrefs;

namespace Commons.Debugging.SettingsEditor.UI
{
    public class UiVisibilitySettings : ISettings
    {
        public readonly GameObject[] Objects;

        public UiVisibilitySettings(IGetGameObjects getter)
        {
            Objects = getter.GetGameObjects();
        }

        public void Load()
        {
            foreach (GameObject go in Objects)
                go.SetActive(PrefsUtils.GetBool(go.name, true));
        }

        public void Save()
        {
            foreach (GameObject go in Objects)
                PrefsUtils.SetBool(go.name, go.activeSelf);
        }

        public void Invalidate()
        {
        }

        public override string ToString()
        {
            if (Objects == null)
                return base.ToString();

            StringBuilder sb = new StringBuilder(TypesUtils.GetSimpleClassName(this));
            sb.Append(" .");

            foreach (GameObject go in Objects)
                sb.Append(go.name).Append(": ").Append(go.activeSelf).Append(". ");

            return sb.ToString();
        }
    }
}