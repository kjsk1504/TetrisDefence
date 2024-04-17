using TetrisDefence.Data.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.Data.Utill
{
    /// <summary>
    /// <see langword="Tab"/>을 이용해 <see cref="Selectable"/>을 선택할 수 있게 함 <br><see cref="MonoBehaviour"/>를 상속 받음</br>
    /// </summary>
    public class FocusControll : MonoBehaviour
    {
        /// <summary> 선택 가능한 게임 오브젝트들 </summary>
        [field: SerializeField] Selectable[] selectables = default;
        /// <summary> <see cref="Selectable"/> 중 선택된 인덱스 </summary>
        private int _selectedIndex = default;
        /// <summary>  확인하는 비동기 함수 </summary>
        private Coroutine _focusUpdate = default;


        /// <summary>
        /// 자식 오브젝트들 중에 <see cref="Selectable"/>을 <see cref="selectables"/>에 저장
        /// </summary>
        private void Awake()
        {
            selectables = GetComponentsInChildren<Selectable>(true);
        }

        /// <summary>
        /// <see cref="C_SetDefFocus"/>를 실행해서 현재 활성화된 오브젝트를 찾고 <see cref="C_FocusUpdate"/>로 
        /// </summary>
        private void OnEnable()
        {
            StartCoroutine(C_SetDefFocus());
            _focusUpdate = StartCoroutine(C_FocusUpdate());
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnDisable()
        {
            StopCoroutine(_focusUpdate);
        }

        /// <summary>
        /// 현재 활성화되어 있는 게임 오브젝트를 찾음
        /// </summary>
        /// <returns></returns>
        private IEnumerator C_SetDefFocus()
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < selectables.Length; i++)
            {
                if (i == _selectedIndex)
                {
                    continue;
                }

                else if (selectables[i].gameObject.activeSelf == true && selectables[i].IsInteractable() == true)
                {
                    selectables[i].Select();

                    _selectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerator C_FocusUpdate()
        {
            while (true)
            {
                KeyDownBind();
                yield return null;
            }
        }

        /// <summary>
        /// <see langword="Tab"/>나 <see langword="Shift+Tab"/>이 눌렸는지 확인
        /// </summary>
        private void KeyDownBind()
        {
            if (InputManager.Instance.IsShiftTabKeyDown)
            {
                MoveBackward();
            }
            else if (InputManager.Instance.IsTabKeyDown)
            {
                MoveForward();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void MoveBackward()
        {
            int idx = 0;
            if (_selectedIndex <= 0)
            {
                idx = selectables.Length - 1;
            }
            else
            {
                idx = _selectedIndex - 1;
            }
            for (int i = idx; i < selectables.Length; i--)
            {
                if (selectables[i].gameObject.activeSelf == true && selectables[i].IsInteractable() == true)
                {
                    selectables[i].Select();

                    _selectedIndex = i;
                    break;
                }
                if (i == 0)
                {
                    i = selectables.Length - 1;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void MoveForward()
        {
            int idx = 0;
            if (_selectedIndex >= selectables.Length - 1)
            {
                idx = 0;
            }
            else
            {
                idx = _selectedIndex + 1;
            }
            for (int i = idx; i < selectables.Length; i++)
            {
                if (selectables[i].gameObject.activeSelf == true && selectables[i].IsInteractable() == true)
                {
                    selectables[i].Select();

                    _selectedIndex = i;
                    break;
                }
            }
        }
    }
}
