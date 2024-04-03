using TetrisDefence.UI.Base;
using TetrisDefence.Data.Manager;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor.PackageManager.Requests;

namespace TetrisDefence.UI
{
    public class LoginUI : UIScreenBase
    {
        [SerializeField] TMP_InputField _id;
        [SerializeField] TMP_InputField _pw;
        [SerializeField] Button _tryLogin;
        [SerializeField] Button _register;
        [SerializeField] WebUserDataRequest _request;


        private void Start()
        {
            _tryLogin.onClick.AddListener(() =>
            {
                _request.OnPostButtonClicked("login", _id.text, _pw.text);
            });

            _register.onClick.AddListener(() =>
            {
                UIManager.instance.Get<UIRegisterWindow>().Show();
            });
        }
    }
}
