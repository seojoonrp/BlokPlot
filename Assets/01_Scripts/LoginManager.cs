using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class UserData
{
    public string username;
    public ProfileData profile;
    public StatsData stats;
}
[System.Serializable]
public class ProfileData { public string avatarUrl, bannerUrl; }
[System.Serializable]
public class StatsData { public int wins, liarWins, trophies, coins; }

public class LoginManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public Button loginButton;

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
    }

    public void OnLoginButtonClick()
    {
        string username = usernameInput.text;
        if (string.IsNullOrEmpty(username))
        {
            Debug.LogError("Username cannot be empty!");
            return;
        }
        StartCoroutine(LoginOrRegisterCoroutine(username));
    }

    IEnumerator LoginOrRegisterCoroutine(string username)
    {
        string url = "http://localhost:8080/api/login";

        string jsonBody = "{\"username\":\"" + username + "\"}";
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
                UserData receivedUser = JsonUtility.FromJson<UserData>(webRequest.downloadHandler.text);

                Debug.Log($"Welcome, {receivedUser.username}! Coins: {receivedUser.stats.coins}");
            }
        }
    }
}
