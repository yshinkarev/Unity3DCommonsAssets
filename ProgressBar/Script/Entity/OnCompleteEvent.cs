using System;
using UnityEngine.Events;

namespace ProgressBar.Entity
{
    /// <summary>
    /// Method chosen to be triggered when a ProgressBar is done.
    /// </summary>
    [Serializable]
    public class OnCompleteEvent : UnityEvent { }
}