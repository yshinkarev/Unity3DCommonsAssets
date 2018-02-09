using Commons.Utils;
using UnityEngine;

namespace Commons.Debugging
{
    public class DebugGameObjectVisibilityParam
    {
        private bool _visible;
        private GameObject[] _objects;

        public DebugGameObjectVisibilityParam()
        {

        }

        public DebugGameObjectVisibilityParam(GameObject go)
        {
            _objects = new GameObject[] { go };
        }

        public DebugGameObjectVisibilityParam(GameObject[] gos)
        {
            _objects = gos;
        }

        #region Properties

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;

                if (_objects == null)
                    return;

                //                Debug.LogFormat("Visible of {0} : {1}", _names, value);

                foreach (GameObject go in _objects)
                {
                    //                    Debug.LogFormat("{0} = {1}", go.name, value);
                    go.SetActive(value);
                }
            }
        }

        public GameObject[] GameObjects
        {
            get { return _objects; }
        }

        #endregion

        public void Invalidate()
        {
            Visible = _visible;
        }


        public DebugGameObjectVisibilityParam SetObjectsByTag(string tag)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);

            if (objects.Length == 0)
                objects = new GameObject[] { GOU.FindInactiveByTag(tag, false /* isRooted */) };

            SetObjects(objects);
            return this;
        }

        public DebugGameObjectVisibilityParam SetObjectsByName(string name)
        {
            SetObjects(new GameObject[] { GOU.FindInactive(name) });
            return this;
        }


        protected void SetObjects(GameObject[] objects)
        {
            _objects = objects;
            _visible = _objects[0].activeSelf;
        }
    }
}