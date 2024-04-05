using TetrisDefence.Data.Utill;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

public class WebUserDataRequest : SingletonMonoBase<WebUserDataRequest>
{
    private Process process;

    // ip 주소.
    [SerializeField] string _url = "localhost";

    // 포트 정보.
    [SerializeField] string _port = "3000";


    private void OnEnable()
    {
        ProcessStartInfo processInfo = new ProcessStartInfo("node.exe", "C:\\Users\\kjsk1\\Documents\\kjs\\Workspace\\Nodejs\\TetrisDefenceServer\\index.js");
        processInfo.CreateNoWindow = false;
        process = Process.Start(processInfo);
    }

    private void OnDisable()
    {
        ProcessStartInfo processInfo = new ProcessStartInfo("taskkill.exe", "/f /im node.exe");
        Process.Start(processInfo);
    }

    private string CreateRequestURL(string path)
    {
        string requestURL = $"{_url}:{_port}/{path}";

        if (_url != "localhost")
        {
            requestURL = "http://" + requestURL;
        }

        return requestURL;
    }

    public void ConnectionCheck()
    {
        StartCoroutine(RequestGet($"{_url}:{_port}"));
    }

    // Get 버튼 눌리면 실행할 함수.
    public void OnGetButtonClicked(string path)
    {
        StartCoroutine(RequestGet(CreateRequestURL(path)));
    }

    IEnumerator RequestGet(string requestURL)
    {
        UnityWebRequest request;

        // 서버에 요청하는 객체 생성.
        request = UnityWebRequest.Get(requestURL);

        // 서버에서 응답이 오기까지 대기 (비동기).
        yield return request.SendWebRequest();

        // 다른 처리.
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("응답 실패");
        }
    }

    // Post 버튼 눌리면 실행할 함수.
    public void OnPostButtonClicked(string path, string id, string pw)
    {
        StartCoroutine(RequestPost(CreateRequestURL(path), id, pw));
    }

    // Post 요청 함수.
    IEnumerator RequestPost(string requestURL, string id, string pw)
    {
        // 폼 객체 생성.
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("pw", pw);

        // Post 요청 객체 생성.
        UnityWebRequest request = UnityWebRequest.Post(requestURL, form);

        // Post 요청 전달.
        yield return request.SendWebRequest();

        // 결과 확인.
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}
