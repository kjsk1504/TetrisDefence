using TetrisDefence.Data.Utill;
using TetrisDefence.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TetrisDefence.Data.Manager
{
    /// <summary>
    /// UI(<see cref="UIBase"/>를 상속 받음)를 하나로 관리 <br><see cref="SingletonMonoBase{T}"/>를 상속 받음</br>
    /// </summary>
    public class UIManager : SingletonMonoBase<UIManager>
    {
        /// <summary> 모든 UI(<see cref="IUI"/>를 상속 받음)가 있는 해쉬테이블</summary>
        private Dictionary<Type, IUI> _uis = new ();
        /// <summary> 모든 Screen UI(<see cref="IUIScreen"/>을 상속 받음)가 있는 동적배열</summary>
        private List<IUIScreen> _screens = new ();
        /// <summary> 모든 PopUp UI(<see cref="IUIPopUp"/>을 상속 받음)가 있는 링크드 리스트</summary>
        private LinkedList<IUIPopUp> _popUps = new ();
        /// <summary> 캔버스의 레이케스트 결과들</summary>
        private List<RaycastResult> _raycastResult = new ();


        private void Update()
        {
            if (_popUps.Count > 0)
            {
                if (_popUps.Last.Value.InputActionEnable)
                    _popUps.Last.Value.InputAction();
            }

            if (_screens.Count > 0)
            {
                foreach (var screen in _screens)
                {
                    screen.InputAction();
                }
            }
        }

        /// <summary>
        /// UI 최초 등록 <br><see cref="IUI"/>를 상속받는 UI가 Awake단계에서 자기 자신을 등록</br>
        /// </summary>
        /// <param name="ui"> 등록할 UI </param>
        /// <exception cref="동일한 UI 가 씬에 두개 이상 존재함. 하나 지워야함"> 동일한 UI가 씬에 두개 이상 존재 </exception>
        public void Register(IUI ui)
        {
            if (_uis.TryAdd(ui.GetType(), ui))
            {
                if (ui is IUIScreen)
                {
                    _screens.Add((IUIScreen)ui);
                }
            }
            else
            {
                throw new Exception($"[UIManager] : Failed to register {ui.GetType()}... already exist.");
            }
        }

        /// <summary>
        /// UI 검색 <br><see cref="IUI"/>를 상속 받는 UI의 타입을 통해 검색</br>
        /// </summary>
        /// <typeparam name="T"> 가져오려는 UI의 타입 </typeparam>
        /// <returns> UI 오브젝트 </returns>
        /// <exception cref="가져오려는 UI 가 존재하지 않음"> 가져오려는 UI가 존재하지 않음 </exception>
        public T Get<T>()
            where T : IUI
        {
            if (_uis.TryGetValue(typeof(T), out IUI ui))
            {
                return (T)ui;
            }
            else
            {
                throw new Exception($"[UIManager] : Failed to get {typeof(T)}... not exist.");
            }
        }

        /// <summary>
        /// 새로 보여줄 PopUp(<see cref="UIPopUpBase"/>을 상속 받음)을 정렬순서 가장 뒤로 보냄
        /// </summary>
        /// <param name="ui"> 새로 보여줄 PopUp </param>
        public void Push(IUIPopUp ui)
        {
            int sortingOrder = 1;

            if (_popUps.Last?.Value != null)
            {
                _popUps.Last.Value.InputActionEnable = false;
                sortingOrder = _popUps.Last.Value.SortingOrder + 1;
            }

            ui.InputActionEnable = true;
            ui.SortingOrder = sortingOrder;
            _popUps.Remove(ui);
            _popUps.AddLast(ui);

            if (sortingOrder > 256)
            {
                RearrangePopUpSortingOrders();
            }

            if (_popUps.Count == 1)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        /// <summary>
        /// PopUp(<see cref="UIPopUpBase"/>을 상속 받음)을 제거. 이전 PopUp이 있다면 이전 PopUp의 입력처리 활성화.
        /// </summary>
        /// <param name="ui"> 제거할 popup </param>
        public void Pop(IUIPopUp ui)
        {
            // 제거 하려는 Popup이 마지막이면서 이전 Popup이 있다면 이전 PopUp의 입력처리 활성화.
            if (_popUps.Count >= 2 && _popUps.Last.Value == ui)
                _popUps.Last.Previous.Value.InputActionEnable = true;

            _popUps.Remove(ui); // 제거

            if (_popUps.Count == 0)
            {
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
            }
        }

        /// <summary>
        /// 현재 포인터 위치에 다른 캔버스의 RaycastTarget이 있는지 검출
        /// <br>(마우스포인터가 현재 캔버스의 <see langword="UI Componet"/> 말고 다른 캔버스의 <see langword="UI Componet"/> 위에 있는지 검출)</br>
        /// </summary>
        /// <param name="ui"> 현재 ui </param>
        /// <param name="other"> 다른 캔버스 ui </param>
        /// <param name="hovered"> 마우스가 올라가 있는 캔버스 </param>
        /// <returns> 검출 여부 </returns>
        public bool TryCastOther(IUI ui, out IUI other, out GameObject hovered)
        {
            other = null;
            hovered = null;
            _raycastResult.Clear();

            for (LinkedListNode<IUIPopUp> node = _popUps.Last; node != null; node = node.Previous)
            {
                node.Value.Raycast(_raycastResult);

                if (_raycastResult.Count > 0)
                {
                    hovered = _raycastResult[0].gameObject;

                    if (node.Value == ui)
                    {
                        return false;
                    }
                    other = hovered.transform.root.GetComponent<IUI>();
                    return true;
                }
            }

            foreach (var screen in _screens)
            {
                screen.Raycast(_raycastResult);

                if (_raycastResult.Count > 0)
                {
                    hovered = _raycastResult[0].gameObject;

                    if (screen == ui)
                    {
                        return false;
                    }

                    other = hovered.transform.root.GetComponent<IUI>();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 모든 UI(<see cref="_popUps"/>과 <see cref="_screens"/>)를 순회하면서 레이케스트 여부를 확인
        /// </summary>
        /// <returns></returns>
        public List<RaycastResult> RayCastAll()
        {
            _raycastResult.Clear();

            for (LinkedListNode<IUIPopUp> node = _popUps.Last; node != null; node = node.Previous)
            {
                node.Value.Raycast(_raycastResult);
            }

            foreach (var screen in _screens)
            {
                screen.Raycast(_raycastResult);
            }

            return _raycastResult;
        }

        /// <summary>
        /// PopUp UI(<see cref="UIPopUpBase"/>를 상속 받음)들의 SortingOrder를 다시 정렬함
        /// </summary>
        private void RearrangePopUpSortingOrders()
        {
            int sortingOrder = 1;
            foreach (var popUp in _popUps)
            {
                popUp.SortingOrder = sortingOrder++;
            }
        }
    }
}
