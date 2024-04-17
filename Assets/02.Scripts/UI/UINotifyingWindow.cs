using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace TetrisDefence.UI
{
    /// <summary>
    /// 알림 창 (메세지를 보여주고 확인 버튼을 가짐, 일정 시간 후 사라짐)
    /// <br><see cref="UIPopUpBase"/>를 상속 받음</br>
    /// </summary>
    public class UINotifyingWindow : UIPopUpBase
    {
        /// <summary> 메세지 영역 </summary>
        private TMP_Text _message;
        /// <summary> 확인 버튼 </summary>
        private Button _confirm;
        /// <summary> 사라질 시간 </summary>
        private float _hideSecond = default;


        protected override void Awake()
        {
            base.Awake();

            _message = transform.Find("Panel/Text (TMP) - Message").GetComponent<TMP_Text>();
            _confirm = transform.Find("Panel/Button - Confirm").GetComponent<Button>();
        }

        /// <summary>
        /// 알림 창을 띄움
        /// </summary>
        /// <param name="message"> 보여줄 메세지 </param>
        /// <param name="onConfirm"> 확인 버튼 클릭시 작동할 대리자 </param>
        /// <param name="hideSecond"> 사라질 시간(초) </param>
        public void Show(string message, UnityAction onConfirm = null, float hideSecond = 1.0f)
        {
            _message.text = message;

            _confirm.onClick.RemoveAllListeners();
            _confirm.onClick.AddListener(Hide);

            if (onConfirm != null)
            {
                _confirm.onClick.AddListener(onConfirm);
            }

            base.Show();
            _hideSecond = hideSecond;
            Invoke(nameof(Hide), _hideSecond);
        }

        public override void Hide()
        {
            CancelInvoke(nameof(Hide));

            base.Hide();
        }
    }
}