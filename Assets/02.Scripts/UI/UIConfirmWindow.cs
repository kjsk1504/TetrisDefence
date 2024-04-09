using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TetrisDefence.UI
{
    /// <summary>
    /// 확인 창 (메세지를 보여주고 확인 버튼과 취소 버튼을 가짐)
    /// <br><see cref="UIPopUpBase"/>를 상속 받음</br>
    /// </summary>
    public class UIConfirmWindow : UIPopUpBase
    {
        /// <summary> 메세지 영역 </summary>
        private TMP_Text _message;
        /// <summary> 확인 버튼 </summary>
        private Button _confirm;
        /// <summary> 취소 버튼 </summary>
        private Button _cancel;


        protected override void Awake()
        {
            base.Awake();

            _message = transform.Find("Panel/Text (TMP) - Message").GetComponent<TMP_Text>();
            _confirm = transform.Find("Panel/Button - Confirm").GetComponent<Button>();
            _cancel = transform.Find("Panel/Button - Cancel").GetComponent<Button>();
        }

        /// <summary>
        /// 확인 창을 띄움
        /// </summary>
        /// <param name="message"> 보여줄 메세지 </param>
        /// <param name="onConfirm"> 확인 버튼 클릭시 작동할 대리자 함수 </param>
        /// <param name="onCancel"> 취소 버튼 클릭시 작동할 대리자 함수 </param>
        public void Show(string message, UnityAction onConfirm, UnityAction onCancel = null)
        {
            _message.text = message;

            _confirm.onClick.RemoveAllListeners();
            _confirm.onClick.AddListener(Hide);

            if (onConfirm != null)
            {
                _confirm.onClick.AddListener(onConfirm);
            }

            _cancel.onClick.RemoveAllListeners();
            _cancel.onClick.AddListener(Hide);

            if (onCancel != null)
            {
                _cancel.onClick.AddListener(onCancel);
            }

            base.Show();
        }
    }
}