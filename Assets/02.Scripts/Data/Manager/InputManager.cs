using TetrisDefence.Data.Utill;
using UnityEngine;

namespace TetrisDefence.Data.Manager
{
    public class InputManager : SingletonMonoBase<InputManager>
    { 
        public string InputString { get; private set; } = string.Empty;
        public Vector3 MousePosition { get; private set; } = Vector3.zero;
        public float Horizontal { get; private set; } = float.NaN;
        public float Vertical { get; private set; } = float.NaN;
        public bool IsTabDown { get; private set; } = false;
        public bool IsShiftTabDown { get; private set; } = false;
        public bool IsEnterDown { get; private set; } = false;
        public bool IsLeftClicked { get; private set; } = false;
        public bool IsRightClicked { get; private set; } = false;

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
