using TetrisDefence.Data.Manager;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TetrisDefence.UI
{
    public class LoginUI : UIScreenBase
    {
        [SerializeField] TMP_InputField _id;
        [SerializeField] TMP_InputField _pw;
        [SerializeField] Button _tryLogin;
        [SerializeField] Button _register;


        private void Start()
        {
            _tryLogin.onClick.AddListener(() =>
            {
                WebUserDataRequest.Instance.OnPostButtonClicked("login", _id.text, _pw.text);
            });

            _register.onClick.AddListener(() =>
            {
                UIManager.Instance.Get<UIRegisterWindow>().Show();
            });

            WebUserDataRequest.Instance.ConnectionCheck();
        }
    }
}
