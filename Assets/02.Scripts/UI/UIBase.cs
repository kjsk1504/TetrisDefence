using TetrisDefence.Data.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TetrisDefence.UI
{
    /// <summary>
    /// <see langword="Canvas"/> 관리용 기본 단위 컴포넌트
    /// <br><see cref="MonoBehaviour"/>와 <see cref="IUI"/>를 상속 받음</br>
    /// </summary>
    public abstract class UIBase : MonoBehaviour, IUI
    {
        public int SortingOrder 
        { 
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }
        public bool InputActionEnable { get; set; }
        public event Action onShow;
        public event Action onHide;

        protected Canvas canvas;
        protected GraphicRaycaster raycastModule;


        protected virtual void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            raycastModule = GetComponent<GraphicRaycaster>();
            UIManager.Instance.Register(this);
        }

        /// <summary>
        /// 이 UI를 토글함 (꺼져있으면 키고, 켜져있으면 끔)
        /// </summary>
        public void Toggle()
        {
            if (canvas.enabled)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public virtual void InputAction()
        {
        }

        public virtual void Show()
        {
            if (canvas.enabled == false)
            {
                canvas.enabled = true;
                onShow?.Invoke();
            }
        }

        public virtual void Hide()
        {
            if (canvas.enabled == true)
            {
                canvas.enabled = false;
                onHide?.Invoke();
            }
        }

        public void Raycast(List<RaycastResult> results)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            raycastModule.Raycast(pointerEventData, results);
        }
    }
}
