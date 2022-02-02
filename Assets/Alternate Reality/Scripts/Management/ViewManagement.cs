using AlternateReality.View;
using System.Collections.Generic;
using UnityEngine;

namespace AlternateReality.Management
{
    public class ViewManagement : MonoBehaviour
    {
        public static ViewManagement Instance;

        private Dictionary<string, BaseView> _views;


        private void Awake()
        {
            if (!Instance)
                Instance = this;

            _views = new Dictionary<string, BaseView>();

            foreach (RectTransform child in transform)
            {
                child.gameObject.SetActive(false);

                if (_views.ContainsKey(child.name))
                {
                    child.name = child.name + " (Duplicate View)";

                    Debug.LogWarning("<color=yellow>[ViewManagement] Awake() -> There is already a view with the key '" + child.name + "'. The view was NOT added and has been marked in the hierarchy.</color>");

                    continue;
                }

                BaseView view = child.GetComponent<BaseView>();

                if (view)
                {
                    view.Deactivate();

                    _views.Add(child.name, view);
                }
                else
                {
                    child.name = child.name + " (Missing BaseView)";

                    Debug.LogWarning("<color=yellow>[ViewManagement] Awake() -> A view with the key '" + child.name + "' has no BaseView attached. The view was NOT added and has been marked in the hierarchy.</color>");
                }
            }

            SetView(Views.MAIN_VIEW);
        }


        public void SetView(string to)
        {
            if (!_views.ContainsKey(to))
            {
                Debug.LogWarning("<color=yellow>[ViewManagement] SetView() -> No view with key '" + to + "' was found.</color>");
                return;
            }

            if (ActiveView)
            {
                ActiveView.Deactivate();
                ActiveView.gameObject.SetActive(false);
            }
            
            ActiveView = _views[to];
            ActiveView.gameObject.SetActive(true);
            ActiveView.Activate();
        }


        public BaseView ActiveView { get; private set; }
    }
}