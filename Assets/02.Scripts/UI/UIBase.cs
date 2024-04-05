using TetrisDefence.Data.Manager;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TetrisDefence.UI
{
    /// <summary>
    /// Canvas 관리용 기본 단위 컴포넌트
    /// </summary>
    public abstract class UIBase : MonoBehaviour, IUI
    {
        public int sortingOrder 
        { 
            get => canvas.sortingOrder;
            set => canvas.sortingOrder = value;
        }
        public bool inputActionEnable { get; set; }

        protected Canvas canvas;
        protected GraphicRaycaster raycastModule;

        public event Action onShow;
        public event Action onHide;


        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
            raycastModule = GetComponent<GraphicRaycaster>();
            UIManager.Instance.Register(this);
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

        public void Toggle()
        {
            if (canvas.enabled)
                Hide();
            else
                Show();
        }

        public void Raycast(List<RaycastResult> results)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            raycastModule.Raycast(pointerEventData, results);
        }
    }
}