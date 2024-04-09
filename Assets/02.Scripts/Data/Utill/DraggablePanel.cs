using TetrisDefence.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TetrisDefence.Data.Utill
{
    /// <summary>
    /// 드래그 가능한 패널 <br><see cref="MonoBehaviour"/>와 <see cref="IDragHandler"/>를 상속 받음</br>
    /// </summary>
    public class DraggablePanel : MonoBehaviour, IDragHandler
    {
        /// <summary> 닫았다 열때마다 위치를 초기화할지 여부 </summary>
        [SerializeField] bool _resetPositionOnEnabled = true;
        /// <summary> 패널의 트랜스폼 컴포넌트 </summary>
        private RectTransform _rectTransform = default;
        /// <summary> 패널의 시작 위치 </summary>
        private Vector2 _origin = default;


        /// <summary>
        /// 패널의 트랜스 폼 컴포넌트를 받고 시작 위치를 받은 후 <br>만약 <see cref="_resetPositionOnEnabled"/>가 <see langword="true"/>면 다시 켰을때 위치를 시작 위치로 설정</br>
        /// </summary>
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _origin = _rectTransform.anchoredPosition;

            if (transform.root.TryGetComponent(out IUI ui))
            {
                ui.onShow += () =>
                {
                    if (_resetPositionOnEnabled)
                    {
                        _rectTransform.anchoredPosition = _origin;
                    }
                };
            }
        }

        /// <summary>
        /// 드래그 했을때 패널의 위치를 마우스 포인터의 위치와 같게 함
        /// </summary>
        /// <param name="eventData"> 마우스 포인터의 이벤트 데이터 </param>
        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }
    }
}