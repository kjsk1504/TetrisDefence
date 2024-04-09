using TetrisDefence.Data.Manager;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor.VersionControl;

namespace TetrisDefence.UI
{
    /// <summary>
    /// 로그인 스크린 UI
    /// <br><see cref=" UIScreenBase"/>를 상속 받음</br>
    /// </summary>
    public class LoginUI : UIScreenBase
    {
        /// <summary> 아이디 입력 필드 </summary>
        private TMP_InputField _id;
        /// <summary> 패스워드 입력 필드 </summary>
        private TMP_InputField _pw;
        /// <summary> 로그인 시도 버튼 </summary>
        private Button _tryLogin;
        /// <summary> 가입창 띄우는 버튼 </summary>
        private Button _register;


        protected override void Awake()
        {
            base.Awake();

            _id = transform.Find("LoginPanel/ID Panel/InputField (TMP)").GetComponent<TMP_InputField>();
            _pw = transform.Find("LoginPanel/PW Panel/InputField (TMP)").GetComponent<TMP_InputField>();
            _tryLogin = transform.Find("LoginPanel/Button - Login").GetComponent<Button>();
            _register = transform.Find("LoginPanel/Button - Register").GetComponent<Button>();

            _tryLogin.onClick.AddListener(() =>
            {
                WebDataRequest.Instance.OnPostButtonClicked("login", _id.text, _pw.text);
            });

            _register.onClick.AddListener(() =>
            {
                UIManager.Instance.Get<UIRegisterWindow>().Show();
            });

            WebDataRequest.Instance.ConnectionCheck();
        }
    }
}
