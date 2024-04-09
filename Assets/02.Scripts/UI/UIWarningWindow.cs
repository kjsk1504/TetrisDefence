using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace TetrisDefence.UI
{
    /// <summary>
    /// 경고 창 (보여줄 메세지와 확인 버튼을 가짐)
    /// <br><see cref="UIPopUpBase"/>를 상속 받음</br>
    /// </summary>
    public class UIWarningWindow : UIPopUpBase
    {
        /// <summary> 메세지 영역 </summary>
        private TMP_Text _message;
        /// <summary> 확인 버튼 </summary>
        private Button _confirm;


        protected override void Awake()
        {
            base.Awake();

            _message = transform.Find("Panel/Text (TMP) - Message").GetComponent<TMP_Text>();
            _confirm = transform.Find("Panel/Button - Confirm").GetComponent<Button>();
        }

        /// <summary>
        /// 경고 창을 띄움
        /// </summary>
        /// <param name="message"> 보여줄 메세지 </param>
        /// <param name="onConfirm"> 확인 버튼 클릭시 작동할 대리자 함수 </param>
        public void Show(string message, UnityAction onConfirm = null)
        {
            _message.text = message;

            _confirm.onClick.RemoveAllListeners();
            _confirm.onClick.AddListener(Hide);

            if (onConfirm != null)
            {
                _confirm.onClick.AddListener(onConfirm);
            }

            base.Show();
        }
    }
}