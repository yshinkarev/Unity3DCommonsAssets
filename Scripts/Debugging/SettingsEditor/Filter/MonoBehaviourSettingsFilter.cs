using System;
using System.Reflection;
using Commons.Attributes;

namespace Commons.Debugging.SettingsEditor.Filter
{
    class MonoBehaviourSettingsFilter : DebugSettingsFilter
    {
        private readonly MonoBehaviourSettingsEntity _entity;

        public MonoBehaviourSettingsFilter(MonoBehaviourSettingsEntity entity)
        {
            _entity = entity;
        }

        protected override bool AcceptMember(MemberInfo mi, Type memberType)
        {
            if (!base.AcceptMember(mi, memberType))
                return false;

            if (mi.GetCustomAttributes(typeof(IgnoreToSave), true).Length > 0)
                _entity.IgnoreToSaveMemberNames.Add(mi.Name);

            return true;
        }
    }
}
