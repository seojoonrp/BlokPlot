using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AuthService
{
    private const string ApiUrl = "http://localhost:8080/api/login";

    [System.Serializable]
    private class LoginRequestBody { public string username; }

    public IEnumerator LoginOrRegister(string username, Action<UserData> onSuccess, Action<string> onError)
    {
        LoginRequestBody requestBody = new() { username = username };
        string jsonBody = JsonUtility.ToJson(requestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest webRequest = new(ApiUrl, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                onError?.Invoke("로그인에 실패했습니다. 서버 상태를 확인해주세요.");
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                UserData receivedUser = JsonUtility.FromJson<UserData>(webRequest.downloadHandler.text);
                onSuccess?.Invoke(receivedUser);
            }
        }
    }
}