using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AlternateReality.View
{
    public class EndView : BaseView
    {
        [SerializeField] private Button _btnRetry;
        [SerializeField] private TMP_Text _txtInfo;


        public override void Initialize(params object[] list)
        {
            base.Initialize(list);

            _txtInfo.text = "Game over!\r\nScore: " + list?[0];
        }

        public override void Activate()
        {
            _btnRetry.onClick.AddListener(() => SceneManager.LoadScene("DevelopmentScene"));
        }

        public override void Deactivate()
        {
            _btnRetry.onClick.RemoveAllListeners();
        }
    }
}