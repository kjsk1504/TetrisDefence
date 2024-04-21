using TetrisDefence.Data.Utill;
using UnityEngine;

namespace TetrisDefence.Data.Manager
{
    /// <summary>
    /// 키보드와 마우스 입력을 모두 하나로 관리 <br><see cref="SingletonMonoBase{T}"/>를 상속 받음</br>
    /// </summary>
    public class InputManager : SingletonMonoBase<InputManager>
    {
        /// <summary> 키보드 <see cref="KeyCode.LeftControl"/>키와 <see cref="KeyCode.BackQuote"/>가 눌렸는지 여부 </summary>
        public bool DevConsole { get; private set; } = default;
        /// <summary> 매프레임마다 키보드로 입력한 키 </summary>
        public string InputString { get; private set; } = default;
        /// <summary> 현재 픽셀 좌표상의 마우스 위치 </summary>
        public Vector3 MousePosition { get; private set; } = default;
        /// <summary> 키보드나 마우스가 눌렸는지 여부 </summary>
        public bool IsAnyKeyDown { get; private set; } = default;
        /// <summary> 수평 방향 입력 </summary>
        public float HorizontalAxis { get; private set; } = default;
        /// <summary> 수직 방향 입력 </summary>
        public float VerticalAxis { get; private set; } = default;
        /// <summary> 키보드 <see langword="Tab"/>키가 눌렸는지 여부 </summary>
        public bool IsTabKeyDown { get; private set; } = default;
        /// <summary> 키보드 <see langword="Shift+Tab"/>키가 눌렸는지 여부(왼쪽, 오른쪽 둘 다) </summary>
        public bool IsShiftTabKeyDown { get; private set; } = default;
        /// <summary> 키보드 <see langword="Enter"/>키가 눌렸는지 여부(키패드의 enter도 포함) </summary>
        public bool IsEnterKeyDown { get; private set; } = default;
        /// <summary> 키보드 <see cref="KeyCode.Escape"/>키가 눌렸는지 여부 </summary>
        public bool IsESCKeyDown { get; private set; } = default;
        /// <summary> 키보드 <see cref="KeyCode.Space"/>키가 눌렸는지 여부 </summary>
        public bool IsSpaceBarKeyDown { get; private set; } = default;
        /// <summary> 키보드 <see cref="KeyCode.R"/>키가 눌렸는지 여부 </summary>
        public bool isRKeyDown { get; private set; } = default;
        /// <summary> 마우스 왼쪽 버튼이 눌렸는지 여부 </summary>
        public bool IsMouseLeftClickedDown { get; private set; } = default;
        /// <summary> 마우스 왼쪽 버튼이 눌리는 중인지 여부 </summary>
        public bool IsMouseLeftClicking { get; private set; } = default;
        /// <summary> 마우스 왼쪽 버튼이 눌렸다 떼졌는지 여부 </summary>
        public bool IsMouseLeftClickedUp { get; private set; } = default;
        /// <summary> 마우스 오른쪽 버튼이 눌렸는지 여부 </summary>
        public bool IsMouseRightClickedDown { get; private set; } = default;
        /// <summary> 마우스 오른쪽 버튼이 눌리는 중인지 여부 </summary>
        public bool IsMouseRightClicking { get; private set; } = default;
        /// <summary> 마우스 오른쪽 버튼이 눌렸다 떼졌는지 여부 </summary>
        public bool IsMouseRightClickedUp { get; private set; } = default;
        /// <summary> 마우스 가운데 버튼이 눌렸는지 여부 </summary>
        public bool IsMouseMiddleClickedDown { get; private set; } = default;
        /// <summary> 마우스 가운데 버튼이 눌리는 중인지 여부 </summary>
        public bool IsMouseMiddleClicking { get; private set; } = default;
        /// <summary> 마우스 가운데 버튼이 눌렸다 떼졌는지 여부 </summary>
        public bool IsMouseMiddleClickedUp { get; private set; } = default;
        /// <summary> 마우스 4번째 버튼이 눌렸는지 여부 </summary>
        public bool IsMouseFourthClickedDown { get; private set; } = default;
        /// <summary> 마우스 4번째 버튼이 눌리는 중인지 여부 </summary>
        public bool IsMouseFourthClicking { get; private set; } = default;
        /// <summary> 마우스 4번째 버튼이 눌렸다 떼졌는지 여부 </summary>
        public bool IsMouseFourthClickedUp { get; private set; } = default;
        /// <summary> 마우스 5번째 버튼이 눌렸는지 여부 </summary>
        public bool IsMouseFifthClickedDown { get; private set; } = default;
        /// <summary> 마우스 5번째 버튼이 눌리는 중인지 여부 </summary>
        public bool IsMouseFifthClicking { get; private set; } = default;
        /// <summary> 마우스 5번째 버튼이 눌렸다 떼졌는지 여부 </summary>
        public bool IsMouseFifthClickedUp { get; private set; } = default;


        private void Update()
        {
            DevConsole = Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.BackQuote);
            InputString = Input.inputString;
            MousePosition = Input.mousePosition;
            IsAnyKeyDown = Input.anyKeyDown;
            HorizontalAxis = Input.GetAxis("Horizontal");
            VerticalAxis = Input.GetAxis("Vertical");
            IsTabKeyDown = Input.GetKeyDown(KeyCode.Tab) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            IsShiftTabKeyDown = Input.GetKeyDown(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            IsEnterKeyDown = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
            IsESCKeyDown = Input.GetKeyDown(KeyCode.Escape);
            IsSpaceBarKeyDown = Input.GetKeyDown(KeyCode.Space);
            isRKeyDown = Input.GetKeyDown(KeyCode.R);
            IsMouseLeftClickedDown = Input.GetMouseButtonDown(0);
            IsMouseLeftClicking = Input.GetMouseButton(0);
            IsMouseLeftClickedUp = Input.GetMouseButtonUp(0);
            IsMouseRightClickedDown = Input.GetMouseButtonDown(1);
            IsMouseRightClicking = Input.GetMouseButton(1);
            IsMouseRightClickedUp = Input.GetMouseButtonUp(1);
            IsMouseMiddleClickedDown = Input.GetMouseButtonDown(2);
            IsMouseMiddleClicking = Input.GetMouseButton(2);
            IsMouseMiddleClickedUp = Input.GetMouseButtonUp(2);
            IsMouseFourthClickedDown = Input.GetMouseButtonDown(3);
            IsMouseFourthClicking = Input.GetMouseButton(3);
            IsMouseFourthClickedUp = Input.GetMouseButtonUp(3);
            IsMouseFifthClickedDown = Input.GetMouseButtonDown(4);
            IsMouseFifthClicking = Input.GetMouseButton(4);
            IsMouseFifthClickedUp = Input.GetMouseButtonUp(4);
        }
    }
}
