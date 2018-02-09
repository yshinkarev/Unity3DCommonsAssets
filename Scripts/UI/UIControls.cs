using Commons.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Commons.UI
{
    public class UiControls
    {
        private readonly GameObject _parent;

        public UiControls()
        {
        }

        public UiControls(GameObject parent)
        {
            _parent = parent;
        }

        public GameObject Parent
        {
            get { return _parent; }
        }

        protected Button FindButton(string name)
        {
            return Find<Button>(name);
        }

        protected MyButton FindMyButton(string name)
        {
            return new MyButton(Find(name));
        }

        protected Text FindText(string name)
        {
            return Find<Text>(name);
        }

        protected GameObject Find(string name)
        {
            if (_parent == null)
                return GameObject.Find(name) ?? GOU.FindInactive(name);

            return GOU.FindInactive(name, _parent);
        }

        private T Find<T>(string name)
        {
            return Find(name).GetComponent<T>();
        }
    }
}