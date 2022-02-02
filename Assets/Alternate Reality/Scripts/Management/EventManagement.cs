using AlternateReality.Blocks;
using UnityEngine;

namespace AlternateReality.Management
{
    public class EventManagement : MonoBehaviour
    {
        public delegate void HitBlockEventHandler(BaseBlock block, int row, int column);
        public static event HitBlockEventHandler OnHitBlockEvent;

        
        public static void HitBlockEvent(BaseBlock block, int row, int column)
        {
            OnHitBlockEvent?.Invoke(block, row, column);
        }
    }
}