using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// API 서버에서 받아오는 메세지를 담을 클래스. JSON key 이름과 일치해야 한다.
// JSON 등의 데이터로 변환할 수 있는 객체임을 알려주는 어트리뷰트
[System.Serializable]
public class ServerMessage
{
    public string message;
}

public class APICaller : MonoBehaviour
{   
    public void OnButton1Click()
    {
        StartCoroutine(GetMessageFromServer("http://localhost:8080/button1"));
    }

    public void OnButton2Click()
    {
        StartCoroutine(GetMessageFromServer("http://localhost:8080/button2"));
    }

    IEnumerator GetMessageFromServer(string url)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);

        // 서버에 웹 요청을 보내고, 유니티에게 제어권을 넘겨준다.
        // 응답이 오면 멈췄던 부분부터 코드를 다시 실행함
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            ServerMessage receivedMessage = JsonUtility.FromJson<ServerMessage>(webRequest.downloadHandler.text);
            Debug.Log("Server Message: " + receivedMessage.message);
        }
    }
}
