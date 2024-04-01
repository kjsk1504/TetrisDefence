using TetrisDefence.Data.Manager;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.Data.Utill
{
    public class FocusControll : MonoBehaviour
    {
        [SerializeField]
        Selectable[] selectables;

        int selectedIdx = 0;
        Coroutine focusupdate = null;


        private void Awake()
        {
            selectables = GetComponentsInChildren<Selectable>(true);
        }

        private void OnEnable()
        {
            StartCoroutine(SetDefFocus());
            focusupdate = StartCoroutine(FocusUpdate());
        }

        IEnumerator SetDefFocus()
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < selectables.Length; i++)
            {
                if (selectables[i].gameObject.activeSelf == true && selectables[i].IsInteractable() == true)
                {
                    selectables[i].Select();

                    selectedIdx = i;
                    break;
                }
            }
        }
        private void OnDisable()
        {
            StopCoroutine(focusupdate);
        }

        IEnumerator FocusUpdate()
        {
            while (true)
            {
                KeyDownBind();
                yield return new WaitForSeconds(0);
            }
        }

        private void KeyDownBind()
        {
            if (InputManager.IsShiftTabDown)
            {
                Debug.Log("backward");
                int idx = 0;
                if (selectedIdx <= 0)
                {
                    idx = selectables.Length - 1;
                }
                else
                {
                    idx = selectedIdx - 1;
                }
                for (int i = idx; i < selectables.Length; i--)
                {
                    if (selectables[i].gameObject.activeSelf == true && selectables[i].IsInteractable() == true)
                    {
                        selectables[i].Select();

                        selectedIdx = i;
                        break;
                    }
                    if (i == 0)
                        i = selectables.Length - 1;
                }
            }
            else if (InputManager.IsTabDown)
            {
                Debug.Log("forward");
                int idx = 0;
                if (selectedIdx >= selectables.Length - 1)
                {
                    idx = 0;
                }
                else
                {
                    idx = selectedIdx + 1;
                }
                for (int i = idx; i < selectables.Length; i++)
                {
                    if (selectables[i].gameObject.activeSelf == true && selectables[i].IsInteractable() == true)
                    {
                        selectables[i].Select();

                        selectedIdx = i;
                        break;
                    }
                }

            }
            else if (InputManager.IsEnterDown)
            {
                Button button = GetComponentInChildren<Button>(true);
                if (button != null)
                    button.onClick.Invoke();
            }
        }
    }
}
