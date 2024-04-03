using TetrisDefence.UI.Base;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

namespace TetrisDefence.UI
{
    public class UIRegisterWindow : UIPopUpBase
    {
        [SerializeField] TMP_InputField _id;
        [SerializeField] TMP_InputField _pw;
        [SerializeField] Button _confirm;
        [SerializeField] Button _cancel;
        [SerializeField] Button _xButton;
        [SerializeField] WebUserDataRequest _request;


        protected override void Awake()
        {
            base.Awake();

            _confirm.onClick.AddListener(() =>
            {
                _request.OnPostButtonClicked("signup", _id.text, _pw.text);
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

        private bool IsFormatValid()
        {
            return (_id.text.Length >= 3) && (_pw.text.Length >= 4);
        }
    }
}
