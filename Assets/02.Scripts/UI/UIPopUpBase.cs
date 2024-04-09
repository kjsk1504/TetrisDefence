using TetrisDefence.Data.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TetrisDefence.UI
{
    /// <summary>
    /// 모든 PopUp UI의 부모 클래스
    /// <br><see cref="UIBase"/>와 <see cref="IUIPopUp"/>를 상속 받음</br>
    /// </summary>
    public class UIPopUpBase : UIBase, IUIPopUp
    {
        /// <summary> 바깥을 누르면 숨길지 여부 </summary>
        [SerializeField] bool _hideWhenPointerDownOutside = true;


        public override void InputAction()
        {
            base.InputAction();

            if (InputManager.Instance.IsLeftClicked || InputManager.Instance.IsRightClicked)
            {
                if (UIManager.Instance.TryCastOther(this, out IUI other, out GameObject hovered))
                {
                    if (other is IUIPopUp)
                    {
                        other.Show();
                    }
                }
            }
        }

        public override void Show()
        {
            base.Show();
            UIManager.Instance.Push(this);
        }

        public override void Hide()
        {
            base.Hide();
            UIManager.Instance.Pop(this);
        }

        protected override void Awake()
        {
            base.Awake();
            canvas.enabled = false;

            if (_hideWhenPointerDownOutside)
            {
                CreateOutsidePanel();
            }
        }

        /// <summary>
        /// 마우스가 바깥을 누르면 현재 팝업을 숨기기 위해 패널 생성
        /// </summary>
        private void CreateOutsidePanel()
        {
            GameObject panel = new GameObject("Outside");
            panel.transform.SetParent(transform);
            panel.transform.SetAsFirstSibling();
            panel.AddComponent<Image>().color = new Color(0f, 0f, 0f, 0.4f); // 투명한 하얀색

            RectTransform rectTransform = (RectTransform)panel.transform;
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.localScale = Vector3.one; // 스케일 1, 1, 1 (처음 생성시 0, 0, 0)

            EventTrigger trigger = panel.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener(eventData => Hide());
            trigger.triggers.Add(entry);
        }
    }
}