using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace TetrisDefence.UI
{
    /// <summary>
    /// 가입창 UI
    /// <br><see cref="UIPopUpBase"/>를 상속 받음</br>
    /// </summary>
    public class UIRegisterWindow : UIPopUpBase
    {
        /// <summary> 아이디 입력 필드 </summary>
        private TMP_InputField _id;
        /// <summary> 패스워드 입력 필드 </summary>
        private TMP_InputField _pw;
        /// <summary> 확인 버튼 </summary>
        private Button _confirm;
        /// <summary> 취소 버튼 </summary>
        private Button _cancel;
        /// <summary> X 버튼 </summary>
        private Button _xButton;


        protected override void Awake()
        {
            base.Awake();

            _id = transform.Find("Panel/Panel - ID/InputField (TMP)").GetComponent<TMP_InputField>();
            _pw = transform.Find("Panel/Panel - PW/InputField (TMP)").GetComponent<TMP_InputField>();
            _confirm = transform.Find("Panel/Button - Confirm").GetComponent<Button>();
            _cancel = transform.Find("Panel/Button - Cancel").GetComponent<Button>();
            _xButton = transform.Find("Panel/WindowTitle - Register/Button - X").GetComponent<Button>();

            _confirm.onClick.AddListener(() =>
            {
                WebDataRequest.Instance.OnPostButtonClicked("signup", _id.text, _pw.text);
            });

            _cancel.onClick.AddListener(Hide);
            _xButton.onClick.AddListener(Hide);

            _confirm.interactable = false;
            _id.onValueChanged.AddListener ((value) => _confirm.interactable = IsFormatValid());
            _pw.onValueChanged.AddListener ((value) => _confirm.interactable = IsFormatValid());
        }

        public override void Show()
        {
            base.Show();

            _id.text = string.Empty;
            _pw.text = string.Empty;
        }

        /// <summary>
        /// ID가 3자 이상, PW가 4자 이상인지 여부 확인
        /// </summary>
        /// <returns> 유효성 여부 </returns>
        private bool IsFormatValid()
        {
            return (_id.text.Length >= 3) && (_pw.text.Length >= 4);
        }
    }
}
