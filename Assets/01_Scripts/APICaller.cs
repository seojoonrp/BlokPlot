using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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
