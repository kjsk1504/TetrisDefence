using TetrisDefence.Data.Utill;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

/// <summary>
/// 웹서버에 데이터를 저장이나 검색 요청
/// <br><see cref="SingletonBase{T}"/>를 상속 받음</br>
/// </summary>
public class WebDataRequest : SingletonMonoBase<WebDataRequest>
{
    /// <summary> 웹서버가 실행될 프로세스 </summary>
    private Process process = default;

    /// <summary> ip 주소 (기본값: localhost) </summary>
    [SerializeField] string _url = "localhost";

    /// <summary> 포트 정보 (기본값: 3000) </summary>
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

    /// <summary>
    /// <see langword="URL"/>을 만듬
    /// </summary>
    /// <param name="path"> 추가할 경로 </param>
    /// <returns> 합쳐진 <see langword="URL"/> </returns>
    private string CreateRequestURL(string path)
    {
        string requestURL = $"{_url}:{_port}/{path}";

        if (_url != "localhost")
        {
            requestURL = "http://" + requestURL;
        }

        return requestURL;
    }

    /// <summary>
    /// 웹서버와 연결되었는지 확인
    /// </summary>
    public void ConnectionCheck()
    {
        StartCoroutine(RequestGet($"{_url}:{_port}"));
    }

    /// <summary>
    /// Get요청하는 버튼이 눌리면 실행
    /// </summary>
    /// <param name="path"> 요청 경로 </param>
    public void OnGetButtonClicked(string path)
    {
        StartCoroutine(RequestGet(CreateRequestURL(path)));
    }

    /// <summary>
    /// 웹서버에 Get요청
    /// </summary>
    /// <param name="requestURL"> 요청 <see langword="URL"/> </param>
    /// <returns> 코루틴용 </returns>
    IEnumerator RequestGet(string requestURL)
    {
        UnityWebRequest request;

        request = UnityWebRequest.Get(requestURL);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("응답 실패");
        }
    }

    /// <summary>
    /// Post요청하는 버튼 눌리면 실행
    /// </summary>
    /// <param name="path"> 요청 경로 </param>
    /// <param name="id"> 요청 ID </param>
    /// <param name="pw"> 요청 PW </param>
    public void OnPostButtonClicked(string path, string id, string pw)
    {
        StartCoroutine(RequestPost(CreateRequestURL(path), id, pw));
    }

    /// <summary>
    /// 웹서버에 Post 요청
    /// </summary>
    /// <param name="requestURL"> 요청 <see langword="URL"/> </param>
    /// <param name="id"> 요청 ID</param>
    /// <param name="pw"> 요청 PW</param>
    /// <returns> 코루틴용 </returns>
    IEnumerator RequestPost(string requestURL, string id, string pw)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("pw", pw);

        UnityWebRequest request = UnityWebRequest.Post(requestURL, form);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}