using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Commons.Attributes;
using Commons.Reflection;
using Commons.Reflection.Variable;
using System.ComponentModel;
using Commons.Debugging.Diagnostic;
using Commons.Debugging.SettingsEditor.Filter;
using Commons.Debugging.SettingsEditor.Interfaces;
using UnityEngine;
using PP = UnityEngine.PlayerPrefs;

namespace Commons.Debugging.SettingsEditor
{
    public class MonoBehaviourSettings : ISettings
    {
        private readonly System.Object _obj;
        private readonly IEnumerable<IVariable> _members;
        private readonly MonoBehaviourSettingsEntity _entity = new MonoBehaviourSettingsEntity();

        private readonly bool _ignoreToSave;

        #region Static

        public static MonoBehaviourSettings Find(IEnumerable<ISettings> settings, object obj)
        {
            Type type = obj.GetType();
            return (MonoBehaviourSettings)settings.First(s => s is MonoBehaviourSettings && ((MonoBehaviourSettings)s).MonoBehaviourType == type);
        }

        public static T Find<T>(IEnumerable<ISettings> settings) where T : ISettings
        {
            Type type = typeof(T);
            return (T)settings.First(s => s.GetType() == type);
        }

        #endregion

        public MonoBehaviourSettings(System.Object obj, IVariableFilter filter = null)
        {
            _ignoreToSave = ParseIgnoreToSaveAttr(obj);

            _obj = obj;
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            if (filter == null)
                filter = new MonoBehaviourSettingsFilter(_entity);

            _members = ReflectionUtils.GetVariables(obj, flags, flags | BindingFlags.DeclaredOnly, filter);
        }

        #region Properties.

        public IApplyParamControlValues ApplyParamControlValuesCallback
        {
            set { _entity.ApplyParamControlValuesCallback = value; }
        }

        #endregion

        public Type MonoBehaviourType
        {
            get { return _obj.GetType(); }
        }

        public object GetValue(string memberName)
        {
            return GetMember(memberName).GetValue(_obj);
        }

        public void Load()
        {
            foreach (IVariable var in _members)
            {
                string name = var.GetName();

                if (var.IsInt())
                {
                    if (PP.HasKey(name))
                        var.SetValue(_obj, PP.GetInt(name));
                }
                else if (var.IsFloat())
                {
                    if (PP.HasKey(name))
                        var.SetValue(_obj, PP.GetFloat(name));
                }
                else if (var.IsBool())
                {
                    bool? value = null;

                    if (PP.HasKey(name))
                        value = PP.GetInt(name) != 0;
                    else
                    {
                        var attr = GetAttr<DefaultValueAttribute>(var);

                        if (attr != null && attr.Value is Boolean)
                            value = (bool)attr.Value;
                    }

                    if (value != null)
                        var.SetValue(_obj, value);
                }
                else
                    ThrowIfUnknownType(var);
            }
        }

        public void Save()
        {
            if (_ignoreToSave)
                return;

            foreach (IVariable var in _members)
            {
                string name = var.GetName();

                if (_entity.IgnoreToSaveMemberNames.Contains(name))
                    continue;

                object value = var.GetValue(_obj);

                SaveInternal(var, name, value);
            }
        }

        //        public void SaveSingle(string memberName)
        //        {
        //            IVariable var = GetMember(memberName);
        //            SaveInternal(var, memberName, var.GetValue(_obj));
        //        }

        public void EnumerateAllParams(IEnumerateAllParams callback)
        {
            foreach (IVariable var in _members)
            {
                string name = var.GetName();
                object value = var.GetValue(_obj);

                if (var.IsBool())
                {
                    callback.OnBool(name, (bool)value);
                    continue;
                }

                bool isInt = var.IsInt();
                bool isFloat = var.IsFloat();

                if (!isInt && !isFloat)
                {
                    ThrowIfUnknownType(var);
                    continue;
                }

                if (isInt)
                {
                    RangeIntAttribute range = GetRangeAttr<RangeIntAttribute>(var);

                    int min, max;

                    if (range == null)
                        min = max = -1;
                    else
                    {
                        min = range.Min;
                        max = range.Max;
                    }

                    callback.OnInt(name, (int)value, min, max);
                }
                else
                {
                    RangeFloatAttribute range = GetRangeAttr<RangeFloatAttribute>(var);

                    float min, max;

                    if (range == null)
                        min = max = -1f;
                    else
                    {
                        min = range.Min;
                        max = range.Max;
                    }

                    callback.OnFloat(name, (float)value, min, max, false);
                }
            }
        }

        public void Invalidate()
        {
            IApplyParamControlValues callback = _entity.ApplyParamControlValuesCallback;

            if (callback == null)
                return;

            foreach (IVariable var in _members)
            {
                object value = callback.GetValue(var.GetName());
                Track.Me(Track.SETTINGS, "Set value {0} => {1}", var.GetName(), value);
                var.SetValue(_obj, value);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(MonoBehaviourType.Name);
            sb.Append(". ");

            foreach (IVariable var in _members)
                sb.Append(var.GetName()).Append(": ").Append(var.GetValue(_obj)).Append(". ");

            return sb.ToString();
        }

        #region Private

        private void ThrowIfUnknownType(IVariable var)
        {
            Type type = var.GetVariableType();

            if (type == typeof(GameObject) || type == typeof(string) || type == typeof(Color))
                return;

            throw new Exception(var.GetName() + ": " + type);
        }

        private T GetRangeAttr<T>(IVariable var) where T : Attribute
        {
            T attr = GetAttr<T>(var);

            if (attr == null)
                Track.Me(Track.SETTINGS, "Not set range attr for: {0}", var.GetName());

            return attr;
        }

        private T GetAttr<T>(IVariable var) where T : Attribute
        {
            object[] attrs = var.GetCustomAttributes<T>(true);

            if (attrs.Length == 1)
                return (T)attrs[0];


            return null;
        }

        private void SaveInternal(IVariable var, string name, object value)
        {
            if (var.IsInt())
                PP.SetInt(name, (int)value);
            else if (var.IsFloat())
                PP.SetFloat(name, (float)value);
            else if (var.IsBool())
                PP.SetInt(name, (bool)value ? 1 : 0);
            else
                ThrowIfUnknownType(var);
        }

        private IVariable GetMember(string memberName)
        {
            return _members.First(m => m.GetName() == memberName);
        }

        private bool ParseIgnoreToSaveAttr(System.Object obj)
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(obj.GetType());
            return attrs.OfType<IgnoreToSave>().Any();
        }

        #endregion
    }
}