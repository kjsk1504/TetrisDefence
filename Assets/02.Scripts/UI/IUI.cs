using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace TetrisDefence.UI
{
    /// <summary>
    /// 모든 UI의 기본 상호작용 인터페이스
    /// </summary>
    public interface IUI
    {
        /// <summary> <see cref="UnityEngine.Canvas"/>의 SortOrder </summary>
        int SortingOrder { get; set; }
        /// <summary> InputAction을 수행할지 여부 </summary>
        bool InputActionEnable { get; set; }
        /// <summary>이 UI와 유저가 상호작용 할 때 실행할 내용 </summary>
        void InputAction();

        /// <summary>
        /// 현재 <see cref="UnityEngine.Canvas"/>의 <see cref="UnityEngine.UI.GraphicRaycaster"/> 모듈로 <see langword="RaycastTarget"/>을 감지함
        /// </summary>
        /// <param name="results"> 감지된 결과를 캐싱해 둘 리스트 </param>
        void Raycast(List<RaycastResult> results);

        /// <summary>
        /// 이 UI를 보여줌
        /// </summary>
        void Show();

        /// <summary>
        /// 이 UI를 숨김
        /// </summary>
        void Hide();

        /// <summary> Show 함수가 작동할때 작동하는 대리자 </summary>
        event Action onShow;
        /// <summary> Hide 함수가 작동할때 작동하는 대리자 </summary>
        event Action onHide;
    }
}
