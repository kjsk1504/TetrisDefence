using TetrisDefence.Data.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.Data.Utill
{
    public class DevTest : MonoBehaviour
    {
        public Slider sliderGameSpeed;
        public TMP_Text textGameSpeed;
        public TMP_Text textMousePosition;
        public TMP_Text textInputString;
        private string _oldString = "empty";

        private void Awake()
        {
            sliderGameSpeed.onValueChanged.AddListener((value) =>
            {
                Time.timeScale = value;
                textGameSpeed.text = value.ToString("f2");
            });
        }

        private void Update()
        {
            if (InputManager.Instance.IsAnyKeyDown)
            {
                _oldString = InputManager.Instance.InputString;
            }
            textInputString.text = _oldString;
            textMousePosition.text = InputManager.Instance.MousePosition.ToString("f0");
        }
    }
}
