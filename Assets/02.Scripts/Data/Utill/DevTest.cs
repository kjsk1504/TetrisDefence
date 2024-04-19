using System;
using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Pool;
using TetrisDefence.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.Data.Utill
{
    public class DevTest : MonoBehaviour
    {
        public Slider sliderGameSpeed;
        public TMP_InputField textGameSpeed;
        public TMP_Text textGameTime;
        public Button[] speedButtons;
        public Button[] functionButtons;
        //public TMP_Dropdown spawn;
        public TMP_Text textMousePosition;
        public TMP_Text textInputString;
        private KeyCode _oldKeyCode;
        private EventType _oldKeyType;
        private string _oldString;

        private void Awake()
        {
            sliderGameSpeed.onValueChanged.AddListener((value) =>
            {
                Time.timeScale = value;
                textGameSpeed.text = value.ToString("f2");
            });

            textGameSpeed.onSubmit.AddListener((value) =>
            {
                if (float.TryParse(value, out float result))
                {
                    sliderGameSpeed.value = result;
                }
            });

            speedButtons[0].onClick.AddListener(() =>
            {
                sliderGameSpeed.value = 1;
                speedButtons[0].GetComponent<Image>().color = speedButtons[0].colors.highlightedColor;
                speedButtons[1].GetComponent<Image>().color = speedButtons[1].colors.normalColor;
                speedButtons[2].GetComponent<Image>().color = speedButtons[2].colors.normalColor;
            });
            speedButtons[1].onClick.AddListener(() =>
            {
                sliderGameSpeed.value = 0;
                speedButtons[0].GetComponent<Image>().color = speedButtons[0].colors.normalColor;
                speedButtons[1].GetComponent<Image>().color = speedButtons[1].colors.highlightedColor;
                speedButtons[2].GetComponent<Image>().color = speedButtons[2].colors.normalColor;
            });
            speedButtons[2].onClick.AddListener(() =>
            {
                sliderGameSpeed.value += 1;
                speedButtons[0].GetComponent<Image>().color = speedButtons[0].colors.normalColor;
                speedButtons[1].GetComponent<Image>().color = speedButtons[1].colors.normalColor;
                speedButtons[2].GetComponent<Image>().color = speedButtons[2].colors.highlightedColor;
            });

            functionButtons[0].onClick.AddListener(ShootAllTowers);
            functionButtons[1].onClick.AddListener(BuyAllMinos);
            functionButtons[2].onClick.AddListener(GetMoney);
            functionButtons[3].onClick.AddListener(ClearAllMonster);
            functionButtons[4].onClick.AddListener(SpawnAllMonster);

            //spawn.onValueChanged.AddListener((value) =>
            //{
            //    spawn.value = value;
            //    MobManager.Instance.MobSpawn(value);
            //});
        }

        private void Start()
        {
            _oldString = "empty";
            speedButtons[0].GetComponent<Image>().color = speedButtons[0].colors.highlightedColor;
        }

        private void Update()
        {
            textGameTime.text = $"{(int)Time.fixedTime/60:D2}:{(int)Time.fixedTime%60:D2}:{(int)(Time.fixedTime*100)%100:D2}";

            MouseButtonCilked();

            textInputString.text = _oldString;
            textMousePosition.text = InputManager.Instance.MousePosition.ToString("f0");
        }

        private void OnGUI()
        {
            Event e = Event.current;

            if (e.isKey)
            {
                if (e.keyCode != KeyCode.None)
                {
                    if (e.keyCode == _oldKeyCode && e.type == _oldKeyType)
                    {
                        _oldString = $"{_oldKeyCode} KeyHeld";
                    }
                    else
                    {
                        _oldKeyCode = e.keyCode;
                        _oldKeyType = e.type;

                        _oldString = $"{_oldKeyCode} {_oldKeyType}";
                    }
                }
            }
        }

        private void ShootAllTowers()
        {
            foreach (var tower in TowerManager.Instance.towers)
            {
                tower.Shoot();
            }
        }

        private void BuyAllMinos()
        {
            for (int ix = 0; ix < Enum.GetValues(typeof(EMino)).Length; ix++)
            {
                InventoryManager.Instance.ItemUpdate(ix, +1);
            }
        }

        private void GetMoney()
        {
            GameManager.Instance.MoneyChange(1000);
        }

        private void SpawnAllMonster()
        {
            for (int ix = 3; ix < 9; ix++)
            {
                MobManager.Instance.MobSpawn(ix);
            }
        }

        private void ClearAllMonster()
        {
            for (int ix = 3; ix < 9; ix++)
            {
                PoolManager.Instance.pooledItems[(EItem)ix].Clear();
            }
        }

        private void MouseButtonCilked()
        {
            if (InputManager.Instance.IsMouseLeftClickedDown)
            {
                _oldString = "Left Click Down";
            }
            else if (InputManager.Instance.IsMouseRightClickedDown)
            {
                _oldString = "Right Click Down";
            }
            else if (InputManager.Instance.IsMouseMiddleClickedDown)
            {
                _oldString = "Middle Click Down";
            }
            else if (InputManager.Instance.IsMouseFourthClickedDown)
            {
                _oldString = "Fourth Click Down";
            }
            else if (InputManager.Instance.IsMouseFifthClickedDown)
            {
                _oldString = "Fifth Click Down";
            }
            else if (InputManager.Instance.IsMouseLeftClicking)
            {
                _oldString = "Left Clicking";
            }
            else if (InputManager.Instance.IsMouseRightClicking)
            {
                _oldString = "Right Clicking";
            }
            else if (InputManager.Instance.IsMouseMiddleClicking)
            {
                _oldString = "Middle Clicking";
            }
            else if (InputManager.Instance.IsMouseFourthClicking)
            {
                _oldString = "Fourth Clicking";
            }
            else if (InputManager.Instance.IsMouseFifthClicking)
            {
                _oldString = "Fifth Clicking";
            }
            else if (InputManager.Instance.IsMouseLeftClickedUp)
            {
                _oldString = "Left Click Up";
            }
            else if (InputManager.Instance.IsMouseRightClickedUp)
            {
                _oldString = "Right Click Up";
            }
            else if (InputManager.Instance.IsMouseMiddleClickedUp)
            {
                _oldString = "Middle Click Up";
            }
            else if (InputManager.Instance.IsMouseFourthClickedUp)
            {
                _oldString = "Fourth Click Up";
            }
            else if (InputManager.Instance.IsMouseFifthClickedUp)
            {
                _oldString = "Fifth Click Up";
            }
        }
    }
}
