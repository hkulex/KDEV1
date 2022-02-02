using AlternateReality.Management;
using UnityEngine;
using UnityEngine.UI;

namespace AlternateReality.View
{
    public class MainView : BaseView
    {
        [SerializeField] private Button _btnStart;

        public override void Activate()
        {
            _btnStart.onClick.AddListener(() => ViewManagement.Instance.SetView(Views.GAME_VIEW));
        }

        public override void Deactivate()
        {
            _btnStart.onClick.RemoveAllListeners();
        }
    }
}