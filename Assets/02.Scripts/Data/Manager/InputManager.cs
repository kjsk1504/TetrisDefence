using TetrisDefence.Data.Utill;
using UnityEngine;

namespace TetrisDefence.Data.Manager
{
    /// <summary>
    /// 키보드와 마우스 입력을 모두 하나로 관리 <br><see cref="SingletonMonoBase{T}"/>를 상속 받음</br>
    /// </summary>
    public class InputManager : SingletonMonoBase<InputManager>
    { 
        /// <summary> 매프레임마다 키보드로 입력한 키 </summary>
        public string InputString { get; private set; } = default;
        /// <summary> 현재 픽셀 좌표상의 마우스 위치 </summary>
        public Vector3 MousePosition { get; private set; } = default;
        /// <summary> 수평 방향 입력 </summary>
        public float Horizontal { get; private set; } = default;
        /// <summary> 수직 방향 입력 </summary>
        public float Vertical { get; private set; } = default;
        /// <summary> 키보드 <see langword="Tab"/>키가 눌렸는지 여부 </summary>
        public bool IsTabDown { get; private set; } = default;
        /// <summary> 키보드 <see langword="Shift+Tab"/>키가 눌렸는지 여부(왼쪽, 오른쪽 둘 다) </summary>
        public bool IsShiftTabDown { get; private set; } = default;
        /// <summary> 키보드 <see langword="Enter"/>키가 눌렸는지 여부(키패드의 enter도 포함) </summary>
        public bool IsEnterDown { get; private set; } = default;
        /// <summary> 마우스 왼쪽 버튼이 눌렸는지 여부 </summary>
        public bool IsLeftClicked { get; private set; } = default;
        /// <summary> 마우스 오른쪽 버튼이 눌렸는지 여부 </summary>
        public bool IsRightClicked { get; private set; } = default;

        private void Update()
        {
            InputString = Input.inputString;
            MousePosition = Input.mousePosition;
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            IsTabDown = Input.GetKeyDown(KeyCode.Tab) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            IsShiftTabDown = Input.GetKeyDown(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            IsEnterDown = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
            IsLeftClicked = Input.GetMouseButtonDown(0);
            IsRightClicked = Input.GetMouseButtonDown(1);
        }
    }
}
