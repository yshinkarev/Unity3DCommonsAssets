using System.Collections.Generic;
using Commons.Debugging.SettingsEditor.Interfaces;

namespace Commons.Debugging.SettingsEditor
{
    class MonoBehaviourSettingsEntity
    {
        public readonly HashSet<string> IgnoreToSaveMemberNames = new HashSet<string>();

        public IApplyParamControlValues ApplyParamControlValuesCallback;
    }
}