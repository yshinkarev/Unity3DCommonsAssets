using UnityEngine;
using System.Collections.Generic;

namespace Commons.Touching
{
    public class TouchDispatcher : MonoBehaviour
    {
        private readonly List<ITouchTargetedDelegate> _targetedHandlers = new List<ITouchTargetedDelegate>();
        private readonly List<TouchInfo> _targetedHandlersTouchInfo = new List<TouchInfo>();

        private readonly List<ITouchTargetedDelegate> _targetedHandlersToDel = new List<ITouchTargetedDelegate>();

        private bool _mouseReleased;

        private void Start()
        {
            _mouseReleased = true;
        }

        public void AddTargetedDelegate(ITouchTargetedDelegate intarget, int inTouchPriority, bool inswallowsTouches)
        {
            int i = 0;

            // searching for place to insert delegate
            for (i = 0; i < _targetedHandlers.Count; i++)
            {
                if (_targetedHandlersTouchInfo[i].TouchPriority > inTouchPriority)
                    break;
            }

            _targetedHandlers.Insert(i, intarget);

            TouchInfo newTouchInfo = new TouchInfo
            {
                SwallowsTouches = inswallowsTouches,
                TouchPriority = inTouchPriority
            };

            _targetedHandlersTouchInfo.Insert(i, newTouchInfo);
        }

        public void RemoveDelegate(ITouchTargetedDelegate intarget)
        {
            //add one to remove list
            _targetedHandlersToDel.Add(intarget);
        }

        public void RemoveAllDelegates()
        {
            _targetedHandlers.Clear();
            _targetedHandlersTouchInfo.Clear();
        }

        private void ClearDelList()
        {
            for (int i = 0; i < _targetedHandlersToDel.Count; i++)
            {
                var curTarget = _targetedHandlersToDel[i];
                int index = _targetedHandlers.IndexOf(curTarget);

                _targetedHandlersTouchInfo.RemoveAt(index);
                _targetedHandlers.Remove(curTarget);
            }
            _targetedHandlersToDel.Clear();
        }

        private void Update()
        {
            if (_targetedHandlers.Count > 0)
            {
                MakeDetectionMouseTouch();
            }
            ClearDelList();
        }

        protected virtual void MakeDetectionMouseTouch()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
            MakeDetectionMouse();
#else
        MakeDetectionTouch();
#endif
        }

        protected virtual void MakeDetectionMouse()
        {
            //left mouse button
            if (Input.GetMouseButtonDown(0))
            {
                //мышь не была отжата
                if (!_mouseReleased)
                {
                    TouchCanceled(Input.mousePosition, 1);
                }
                else
                {
                    if (TouchBegan(Input.mousePosition, 1))
                    {
                        _mouseReleased = false;
                    }
                }
            }
            //зажатый компонент
            if (Input.GetMouseButton(0))
            {
                TouchMoved(Input.mousePosition, 1);
            }
            //released button
            if (Input.GetMouseButtonUp(0))
            {
                _mouseReleased = true;
                TouchEnded(Input.mousePosition, 1);
            }
        }

        protected virtual void MakeDetectionTouch()
        {
            int count = Input.touchCount;

            for (int i = 0; i < count; i++)
            {
                Touch touch = Input.GetTouch(i);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        TouchBegan(touch.position, touch.fingerId);
                        break;
                    case TouchPhase.Moved:
                        TouchMoved(touch.position, touch.fingerId);
                        break;
                    case TouchPhase.Ended:
                        TouchEnded(touch.position, touch.fingerId);
                        break;
                    case TouchPhase.Canceled:
                        TouchCanceled(touch.position, touch.fingerId);
                        break;
                }
            }
        }

        //TouchDelegateMethods
        protected virtual bool TouchBegan(Vector2 position, int infingerId)
        {
            for (int i = 0; i < _targetedHandlers.Count; i++)
            {
                if (_targetedHandlers[i].TouchBegan(position, infingerId))
                {
                    _targetedHandlersTouchInfo[i].FingerId[_targetedHandlersTouchInfo[i].CurTouchNumber] = infingerId;
                    _targetedHandlersTouchInfo[i].CurTouchNumber++;

                    if (_targetedHandlersTouchInfo[i].SwallowsTouches)
                        break;
                }
            }
            return true;
        }

        public virtual void TouchMoved(Vector2 position, int infingerId)
        {
            for (int i = 0; i < _targetedHandlers.Count; i++)
            {
                for (int j = 0;
                    j < _targetedHandlersTouchInfo[i].CurTouchNumber && i < _targetedHandlers.Count;
                    j++)
                {
                    if (_targetedHandlersTouchInfo[i].FingerId[j] == infingerId)
                        _targetedHandlers[i].TouchMoved(position, infingerId);
                }
            }
        }

        protected virtual void TouchEnded(Vector2 position, int infingerId)
        {
            for (int i = 0; i < _targetedHandlers.Count; i++)
            {
                for (int j = 0; j < _targetedHandlersTouchInfo[i].CurTouchNumber; j++)
                {
                    if (_targetedHandlersTouchInfo[i].FingerId[j] == infingerId)
                    {
                        _targetedHandlers[i].TouchEnded(position, infingerId);
                        //сместим события тач
                        _targetedHandlersTouchInfo[i].CurTouchNumber--;

                        for (int k = j; k < _targetedHandlersTouchInfo[i].CurTouchNumber; k++)
                        {
                            _targetedHandlersTouchInfo[i].FingerId[k] = _targetedHandlersTouchInfo[i].FingerId[k + 1];
                        }
                    }
                }
            }
        }

        protected virtual void TouchCanceled(Vector2 position, int infingerId)
        {
            for (int i = 0; i < _targetedHandlers.Count; i++)
            {
                for (int j = 0; j < _targetedHandlersTouchInfo[i].CurTouchNumber; j++)
                {
                    if (_targetedHandlersTouchInfo[i].FingerId[j] == infingerId)
                    {
                        _targetedHandlers[i].TouchCanceled(position, infingerId);
                        //сместим события тач
                        _targetedHandlersTouchInfo[i].CurTouchNumber--;

                        for (int k = j; k < _targetedHandlersTouchInfo[i].CurTouchNumber; k++)
                        {
                            _targetedHandlersTouchInfo[i].FingerId[k] = _targetedHandlersTouchInfo[i].FingerId[k + 1];
                        }
                    }
                }
            }
        }
    }
}