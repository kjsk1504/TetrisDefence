using UnityEngine;
using UnityEngine.EventSystems;

namespace TetrisDefence.Game
{
    public class MinoBase : MonoBehaviour, IMino, IDragHandler
    {
        private RectTransform _rectTransform;

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }
    }
}
