using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;


class Utils
{
    public static bool IsStringNull(string testString)
    {
        return string.IsNullOrWhiteSpace(testString) || string.IsNullOrEmpty(testString);
    }
}

public class PostWebRequest : MonoBehaviour
{
    public enum DataStatus
    {
        Error,
        Success,
    }

    [Serializable]
    public class LoginData
    {
        public DataStatus status;
        public string name; // 웹서버의 키와 같아야함
        public string error;
    }

    // ip 주소.
    public string url = "localhost";

    // 포트 정보.
    public string port = "3000";

    public string path;

    // UI InputFields.

    // ID 입력을 위한 입력 필드.
    public TMPro.TMP_InputField idInputField;

    // PW 입력을 위한 입력 필드.
    public TMPro.TMP_InputField pwInputField;

    public LoginData testData;

    //public UnityEvent testEvent;

    //public Dictionary<UnityEngine.UI.Button, UnityAction> dic;


    //private void Awake()
    //{ 
    //    dic = new Dictionary<UnityEngine.UI.Button, UnityAction>();
    //    foreach (var item in dic)
    //    {
    //        item.Key.onClick.AddListener(item.Value);
    //    }
    //}

    //private void Awake()
    //{
    //UnityEngine.UI.Button button = GetComponentInChildren<UnityEngine.UI.Button>();
    //button.onClick.AddListener(OnLoginButtonClicked);
    //}

    //private void Update()
    //{
    //    // 스페이스 키가 눌리면 서버에 Post로 요청 전달.
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        // url:port 구조로 문자열을 만들어서 요청 함수 호출.
    //        StartCoroutine(RequestPost($"{url}:{port}"));
    //    }
    //}

    // 로그인 버튼 눌리면 실행할 함수.
    public void OnLoginButtonClicked()
    {
        path = "Login";
        // 예외 처리.
        if (idInputField == null || pwInputField == null)
        {
            return;
        }

        if (Utils.IsStringNull(idInputField.text)||
            Utils.IsStringNull(pwInputField.text))
        {
            return;
        }

        // 문제 없으면, 로그인 요청 -> 서버로.
        // url:port 구조로 문자열을 만들어서 요청 함수 호출.
        StartCoroutine(RequestPost($"{url}:{port}/{path}"));
    }
    
    // 로그인 버튼 눌리면 실행할 함수.
    public void OnSignupButtonClicked()
    {
        path = "Signup";
        // 예외 처리.
        if (idInputField == null || pwInputField == null)
        {
            return;
        }

        if (Utils.IsStringNull(idInputField.text)||
            Utils.IsStringNull(pwInputField.text))
        {
            return;
        }

        // 문제 없으면, 로그인 요청 -> 서버로.
        // url:port 구조로 문자열을 만들어서 요청 함수 호출.
        StartCoroutine(RequestPost($"{url}:{port}/{path}"));
    }

    // Post 요청 함수.
    IEnumerator RequestPost(string requestURL)
    {
        // 폼 객체 생성.
        WWWForm form = new WWWForm();
        //form.AddField("id", "kjs");
        //form.AddField("pw", "0000");
        form.AddField("id", idInputField.text);
        form.AddField("pw", pwInputField.text);

        // Post 요청 객체 생성.
        UnityWebRequest request = UnityWebRequest.Post(requestURL, form);

        // Post 요청 전달.
        yield return request.SendWebRequest();

        // 결과 확인.
        if (request.result == UnityWebRequest.Result.Success)
        {
            testData = JsonUtility.FromJson<LoginData>(request.downloadHandler.text);
            if (testData.status == DataStatus.Error)
            {
                Debug.Log(testData.error);
            }
            else if (testData.status == DataStatus.Success)
            {
                Debug.Log(testData.name);
            }
        }
    }
}
