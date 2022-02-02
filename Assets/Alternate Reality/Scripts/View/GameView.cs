using AlternateReality.Blocks;
using AlternateReality.Management;
using TMPro;
using UnityEngine;

namespace AlternateReality.View
{
    public class GameView : BaseView
    {
        [SerializeField] private TMP_Text _txtScore;
        
        public override void Activate()
        {
            EventManagement.OnHitBlockEvent += OnUpdateInterface;

            _txtScore.text = "Score:\r\n0";
        }

        private void OnUpdateInterface(BaseBlock block, int row, int column)
        {
            _txtScore.text = "Score:\r\n" + GameManagement.Instance.Score;
        }

        public override void Deactivate()
        {
            EventManagement.OnHitBlockEvent -= OnUpdateInterface;
        }
    }
}