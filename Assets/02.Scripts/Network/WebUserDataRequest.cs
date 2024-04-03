using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebUserDataRequest : MonoBehaviour
{
    // ip 주소.
    [SerializeField] string _url = "localhost";

    // 포트 정보.
    [SerializeField] string _port = "3000";


    // Get 버튼 눌리면 실행할 함수.
    public void OnGetButtonClicked(string path)
    {
        StartCoroutine(RequestGet($"{_url}:{_port}/{path}"));
    }

    IEnumerator RequestGet(string requestURL)
    {
        UnityWebRequest request;
        if (_url == "localhost")
            request = UnityWebRequest.Get(requestURL);

        // 서버에 요청하는 객체 생성.
        request = UnityWebRequest.Get($"http://{requestURL}");

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
        StartCoroutine(RequestPost($"{_url}:{_port}/{path}", id, pw));
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
