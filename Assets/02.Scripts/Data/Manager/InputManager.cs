using TetrisDefence.Data.Base;
using UnityEngine;

namespace TetrisDefence.Data.Manager
{
    public class InputManager : SingletonMonoBase<InputManager>
    {
        public string inputString;
        public Vector3 mousePosition;
        public static float Horizontal { get; private set; } = 0f;
        public static float Vertical { get; private set; } = 0f;
        public static bool IsTabDown { get; private set; } = false;
        public static bool IsShiftTabDown { get; private set; } = false;
        public static bool IsEnterDown { get; private set; } = false;

        private void Update()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            IsTabDown = Input.GetKeyDown(KeyCode.Tab) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            IsShiftTabDown = Input.GetKeyDown(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
            IsEnterDown = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
            inputString = Input.inputString;
            mousePosition = Input.mousePosition;
        }
    }
}
