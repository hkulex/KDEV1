using UnityEngine;

namespace AlternateReality.View
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class BaseView : MonoBehaviour
    {
        public virtual void Initialize(params object[] list) { }
        public abstract void Activate();
        public abstract void Deactivate();
    }
}